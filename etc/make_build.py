#!/usr/bin/python

import sys
import os, errno
import fnmatch
import shutil
import tarfile

#==============================================================================
# Constants
#==============================================================================
ProgramName = "wow"

#==============================================================================
# Functionality
#==============================================================================

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
        print 'rm "%s"' % filename
        os.unlink(filename)

def rmrfdir(dirpath):
    """Extremely dangerous.  Use with caution."""
    if os.path.isdir(dirpath):
        print 'rm -rf "%s"' % dirpath
        shutil.rmtree(dirpath)

def copyfile(src, dst, copymode=True):
    print 'copying "%s"  ->  "%s"' % (src, dst)
    shutil.copymode
    shutil.copyfile(src, dst)
    if copymode:
        shutil.copymode(src, dst)

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
    # deploy each file under 'etc/bin' to '$dst/bin'
    binpath = os.path.join(etcpath, 'bin')
    for filename in os.listdir(binpath):
        filepath = os.path.join(binpath, filename)
        if os.path.isfile(filepath):
            copyfile(filepath, os.path.join(dst, 'bin', filename))

def make_tarfile(output_filename, source_dir):
    """Build a .tar.gz for an entire directory tree."""
    with tarfile.open(output_filename, "w:gz") as tar:
        tar.add(source_dir, arcname=os.path.basename(source_dir))

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
    help="Name the deployment tarball with a version number, like v0.0.01" )


#==============================================================================
# Main
#==============================================================================

def main():
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
    # deploy each file under 'etc/bin' to '$prefix/$vername/$ProgramName/bin'
    etc_deploy(dst, args.etcdir)
    # build the deployment tarball.
    if os.path.exists(tarball):
        raise Exception("Tarball file already exists: %s" % tarball)
    make_tarfile(tarball, base)

if __name__ == "__main__":
    args = parser.parse_args()
    main()

