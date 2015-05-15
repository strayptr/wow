#!/usr/bin/python

import sys
import os, errno
import fnmatch
import shutil
import tarfile
import platform

#==============================================================================
# Constants
#==============================================================================
ProgramName = "wow"

#==============================================================================
# Functionality
#==============================================================================

import re
WS = re.compile(r'\s+', re.MULTILINE)

def contains_whitespace(x):
    return WS.search(x)

def quot(x):
    #return ("%s" % x) if contains_whitespace(x) else x
    return WS.sub('\\ ', x) if contains_whitespace(x) else x

# http://stackoverflow.com/questions/595305/python-path-of-script
def scriptpath():
    """
    Returns the absolute path of the current scriptfile.  
    """
    return os.path.realpath(__file__)

def projectpath():
    """
    Returns the absolute path to our project's root directory, with
    the assumption that this script file is underneath
    '$projectpath/etc/' and that the script will never be located
    within a nested folder named 'etc', e.g. this is not allowed:
    '$projectpath/etc/foo/etc/script.py'
    """
    # get the absolute path to the current scriptfile.
    path = scriptpath()
    # we're somewhere underneath '$projectpath/etc/*', so traverse up
    # until reaching '$projectpath/etc/'
    while os.path.basename(path).lower() != 'etc':
        # if we've reached /etc or / then our assumptions are
        # incorrect.
        assert(os.path.normpath(path) not in [os.path.normpath(p) for p in ['/', '/etc']])
        path = os.path.dirname(path)
    # traverse up once more to reach our project's root dir.
    return os.path.dirname(path)

def query_yes_no(question, default="yes"):
    """Ask a yes/no question via raw_input() and return their answer.

    "question" is a string that is presented to the user.
    "default" is the presumed answer if the user just hits <Enter>.
        It must be "yes" (the default), "no" or None (meaning
        an answer is required of the user).

    The "answer" return value is True for "yes" or False for "no".
    """
    valid = {"yes": True, "y": True, "ye": True,
             "no": False, "n": False}
    if default is None:
        prompt = " [y/n] "
    elif default == "yes":
        prompt = " [Y/n] "
    elif default == "no":
        prompt = " [y/N] "
    else:
        raise ValueError("invalid default answer: '%s'" % default)

    while True:
        sys.stdout.write(question + prompt)
        choice = raw_input().lower()
        if default is not None and choice == '':
            return valid[default]
        elif choice in valid:
            return valid[choice]
        else:
            sys.stdout.write("Please respond with 'yes' or 'no' "
                             "(or 'y' or 'n').\n")

def mkdir_p(path):
    try:
        os.makedirs(path)
    except OSError as exc: # Python >2.5
        if exc.errno == errno.EEXIST and os.path.isdir(path):
            pass
        else: raise

def subfolders(path):
    return [x for x in os.listdir(path) if os.path.isdir(os.path.join(path, x))]

def match_any(name, patterns):
    for pat in patterns:
        if fnmatch.fnmatch(name, pat):
            return True
    return False

def listdir(path, include=['*'], exclude=[]):
    # build a list of all files.
    files = [x for x in os.listdir(path) if os.path.isfile(os.path.join(path, x))]
    # include only the files we care about.
    r = []
    for pat in include:
        r += fnmatch.filter(files, pat)
    r.sort()
    files = list(set(r)) 
    for f in files:
        # exclude files we don't care about.
        if not match_any(f, exclude):
            yield f
            # try to copy an executable by the same name, but no
            # extension.  (The convention I've chosen for a bundled exe.)
            if f.lower().endswith('.exe'):
                bundle = mkpath(os.path.splitext(os.path.join(path, f))[0])
                if os.path.isfile(bundle):
                    yield bundle

def csharp_binfiles(path):
    """Return a list of files that should be bundled for a .NET project.
    
    "path" is a .NET build path like "SlnName/ProjectName/bin/x86/Release"

    """
    if not os.path.isdir(path):
        raise Exception("Not a directory: %s" % path)
    # types of files we want:
    include = [
            # assemblies,
            '*.dll', 
            # executables,
            '*.exe', 
            # and application configuration files (provides 
            # mono with info about how to run the exe)
            '*.exe.config'] 
    # types of files we don't want:
    exclude = [
            # vshost files (created by .NET IDEs for
            # debugging purposes)
            '*.vshost.*' ]
    for f in listdir(path, include=include, exclude=exclude):
        yield f

def getslnpath(slnpath):
    # if slnpath is a file, convert it into the dir the file resides in.
    if os.path.isfile(slnpath):
        slnpath = os.path.dirname(slnpath)
    if not os.path.exists(slnpath):
        raise Exception("No such sln path: %s" % slnpath)
    return slnpath

def sln_binfiles(slnpath):
    slnpath = getslnpath(slnpath)
    # for each project subfolder...
    for project in subfolders(slnpath):
        # yield any project binaries.
        for binpath in ['bin/x86/Release']:
            path = os.path.join(slnpath, project, binpath)
            if os.path.isdir(path):
                for binfile in csharp_binfiles(path):
                    yield ('x86', project, os.path.join(path, binfile))

def rmfile(filename):
    if os.path.exists(filename):
        print 'rm %s' % quot(filename)
        os.unlink(filename)

def rmrfdir(dirpath):
    """Extremely dangerous.  Use with caution."""
    if os.path.isdir(dirpath):
        print 'rm -rf %s' % quot(dirpath)
        shutil.rmtree(dirpath)

def copyfile(src, dst, copymode=True):
    print 'cp %s %s' % (quot(src), quot(dst))
    shutil.copyfile(src, dst)
    if copymode:
        shutil.copymode(src, dst)

def cp_r(src, dst):
    print 'cp -r %s %s' % (quot(src), quot(dst))
    #shutil.copytree(src, dst) # won't work when dst exists, and sadly there's no accepted solution.
    for filename in os.listdir(src):
        dstp = joinpath(dst, filename)
        srcp = joinpath(src, filename)
        if os.path.isfile(srcp):
            mkdir_p(os.path.dirname(dstp))
            copyfile(srcp, dstp)
        elif os.path.isdir(srcp):
            cp_r(srcp, dstp)

def make_tarfile(output_filename, source_dir):
    """Build a .tar.gz for an entire directory tree."""
    with tarfile.open(output_filename, "w:gz") as tar:
        tar.add(source_dir, arcname=os.path.basename(source_dir))
    print 'cd %s; tar cvzf %s %s/' % (
            quot(os.path.dirname(output_filename)),
            quot(os.path.basename(output_filename)),
            quot(os.path.basename(source_dir)))

def sln_deploy(dst, slnpath):
    slnpath = getslnpath(slnpath)
    if not os.path.isdir(dst):
        raise Exception("Not a directory: %s" % dst)
    # deploy each binary.
    for platform, project, binfile in sln_binfiles(slnpath):
        filename = os.path.basename(binfile)
        path = os.path.join(dst, 'bin', platform, project)
        mkdir_p(path)
        copyfile(binfile, os.path.join(path, filename))

def etc_deploy(dst, etcpath):
    # recursively copy 'etc/deploy/*' to '$prefix/$vername/$ProgramName/'
    cp_r(os.path.join(etcpath, 'deploy'), dst)

#==============================================================================
# Cmdline
#==============================================================================
import argparse

parser = argparse.ArgumentParser(formatter_class=argparse.RawTextHelpFormatter, 
    description="""
TODO
""")
     
parser.add_argument('-p', '--prefix',
    default="build",
    help="The destination where the binaries will be placed." )
     
parser.add_argument('-s', '--sourcedir',
    default="src/csharp/Wow",
    help="The directory containing the project's code." )
     
parser.add_argument('-e', '--etcdir',
    default="etc",
    help="The directory containing other deployment files." )
     
parser.add_argument('-v', '--version',
    default="such-signal",
    help="Append the deployment tarball with a version number, such as v0.0.01" )


#==============================================================================
# Main
#==============================================================================

def joinpath(*args):
    return os.path.normpath(os.path.join(*args))

def mkpath(childpath):
    return os.path.normpath(os.path.join(projectpath(), childpath))

def joincmd(*args):
    return ' \\\n'.join(args)

def bundle():
    # windows can't build bundles.
    if platform.system().lower() == 'windows':
        return
    # bundle everything together.
    cmd = []
    flags = ''
    if platform.system().lower() == 'darwin':
        flags += ' -framework CoreFoundation -lobjc -liconv'
        cmd += ['PKG_CONFIG_PATH=/Library/Frameworks/Mono.framework/Versions/Current/lib/pkgconfig/']
    if platform.system().lower() == 'linux':
        cmd += ['CC="cc -arch i386 %s"' % flags]
        cmd += ['AS="as -arch i386"']
    else:
        cmd += ['CC="cc -m32 %s"' % flags]
        cmd += ['AS="as -32 -march i386"']
    path = quot(mkpath('src/csharp/Wow/Visualizer/bin/x86/Release'))
    cmd += ['mkbundle --deps --static']
    cmd += ['%s/Visualizer.exe' % path]
    cmd += ['%s/*.dll' % path]
    cmd += ['-o %s/Visualizer' % path]
    cmd = joincmd(*cmd)
    print cmd
    os.system(cmd)

def gen_binaries():
    slnpath = mkpath('src/csharp/Wow/Wow.sln')
    os.system('xbuild /p:Configuration=Release "%s"' % slnpath)
    bundle()

def gen_build():
    # build the destination directory path.
    vername = '%s-%s' % (ProgramName, args.version)
    # build the deployment tarball name.
    tarball = os.path.join(args.prefix, '%s.tar.gz' % vername)
    # build the base path.
    base = os.path.join(args.prefix, vername)
    base = os.path.normpath(base)
    # if the destination directory already exists, ask the user
    # whether it's okay to delete it.  (WARNING:  This is a very
    # dangerous operation, equivalent to rm -rf, so we need to think
    # carefully about how it's used in order to protect the user.  In
    # this case, we're going to create a subfolder in the prefix path,
    # and only that subfolder will ever be deleted, not the prefix
    # path itself.)
    if os.path.exists(base):
        if not query_yes_no(""""%s" already exists.  Okay to delete?""" % base, default="no"):
            sys.exit(1)
        rmrfdir(base)
        # delete the tarball if it exists.
        rmfile(tarball)
    dst = os.path.join(base, ProgramName)
    # create the destination dir.
    mkdir_p(dst)
    # deploy sln binaries.
    sln_deploy(dst, args.sourcedir)
    # recursively copy the contents of the 'etc/bin' folder '$prefix/$vername/$ProgramName/bin'
    etc_deploy(dst, args.etcdir)
    # build the deployment tarball.
    if os.path.exists(tarball):
        raise Exception("Tarball file already exists: %s" % tarball)
    make_tarfile(tarball, base)

def main():
    gen_binaries()
    gen_build()

if __name__ == "__main__":
    args = parser.parse_args()
    main()

