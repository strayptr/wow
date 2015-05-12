#!/bin/bash
#
# This file is meant to be sourced by your own shell scripts, for
# example:
#
#   source etc/common.sh
#

#==============================================================================
# Provide some useful bash utility functions.
#==============================================================================

#------------------------------------------------------------------------------
# Provide `echoerr`, a way to echo to stderr. Example usage:
#
#   if [ -e "filename.txt" ]; then
#     echoerr "filename.txt not found"
#     exit 1
#   fi
#
# see http://stackoverflow.com/questions/2990414/echo-that-outputs-to-stderr
#------------------------------------------------------------------------------
echoerr() { >&2 echo "$@"; }

# provide a variant that prefixes the msg with the script's filepath.
echoerr2() { >&2 echo "$ScriptParent/$ScriptName: $@"; }

#------------------------------------------------------------------------------
# Provide `panic`, a way to exit after printing an error to stderr.
# Example usage:
#
#   if [ -z "$var" ]; then
#     panic $LINENO "\$var isn't set."
#   fi
#------------------------------------------------------------------------------
panic()
{
  local filename=$(basename "$ScriptPath")
  local line_number=$1; shift 1
  local msg=$@

  #echo "[$filename:$line_number]: $msg" 1>&2
  >&2 echo "`basename \"$ScriptPath\"`/$ScriptName: $@"
  exit 1
}

#------------------------------------------------------------------------------
# Provide `echovars`, a way to print a list of variables for testing
# purposes.
#------------------------------------------------------------------------------
echovars()
{
  for var in $@; do
    echo "$var=${!var}"
  done
}

#------------------------------------------------------------------------------
# Provide `depends_on`, a way for a script to provide a list of
# variable names and fail if any of the variables aren't set.
#------------------------------------------------------------------------------
depends_on()
{
  for var in $@; do
    # see http://unix.stackexchange.com/questions/41292/variable-substitution-with-an-exclamation-mark-in-bash/41293#41293
    if [ -z "${!var}" ] ; then
      echoerr2 "\$$var is blank."
      exit 1
    fi
  done
}


#==============================================================================
# This script relies on realpath, so verify that it's installed and
# suggest ways of installing it.
#==============================================================================
if [ -z "`which realpath`" ]; then
  echoerr "realpath isn't installed."
  echoerr ""
  echoerr "  On Debian-based systems (Ubuntu, etc) try: "
  echoerr "    sudo apt-get install realpath"
  echoerr ""
  echoerr "  On OS X, install Homebrew and then run:"
  echoerr "    brew update"
  echoerr "    brew tap strayptr/tap"
  echoerr "    brew install realpath"
  exit 1
fi

#==============================================================================
# Provide the following two variables:
#
# $ScriptPath - This is the absolute path to the directory
#   containing the current script.
#
# $ScriptName - The filename of the current script.
#
# $ScriptParent - The name of the parent directory of the current
#   script.  (Useful in error messages.)
#
#==============================================================================

# get the full path to the current scriptfile in a way that won't
# break if the path contains spaces.
ScriptPath="`realpath \"$0\"`"
ScriptName="`basename \"$ScriptPath\"`"
ScriptPath="`dirname \"$ScriptPath\"`"
ScriptParent="`basename \"$ScriptPath\"`"

# ensure that neither variable is blank.  This is important in case
# someone runs a command like `rm -rf $ScriptPath/etc`.  See
# https://github.com/valvesoftware/steam-for-linux/issues/3671
depends_on ScriptPath ScriptName           

#==============================================================================
# Load our project's global settings from "config.sh".
#==============================================================================

# try to source the config from our directory.
if [ -e "$ScriptPath/config.sh" ]; then
  source "$ScriptPath/config.sh"
fi

# also try to source the config from our parent directory.  This
# enables organizing your project's scriptfiles into paths like
# `etc/utils/foo.sh`, `etc/build/compile.sh`, etc, while keeping your
# config file at `etc/config.sh`.
if [ -e "$ScriptPath/../config.sh" ]; then
  source "$ScriptPath/../config.sh"
fi

