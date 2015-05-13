
### [git.io/wow](https://git.io/wow) - signal visualizer

`wow` (pronounced "[![Doge](https://git.io/Doge)](//git.io/memes)") is
a tool for viewing and analyzing radio signals captured by SDR, such
as the [HackRF](https://github.com/mossmann/hackrf) via
`hackrf_transfer`.


A secondary objective of the project is to serve as a learning tool.
If you're into SDR, or you're curious about how to write a
cross-platform GUI app, hopefully something in here might be useful
to you.  


### license

MIT.  Use the code however you want.  Take it and build at least n - 1
companies with it.
[![BloodTrail](https://git.io/BloodTrail)](//git.io/memes)

### okay

Currently the software is in pre-alpha.  In fact, it does nothing
whatsoever except display a blank form.  Super useful.

Eventually you'll be able to analyze signal files by running `wow
some-signal-recording.iq`, where `some-signal-recording.iq` is filled
with quadrature samples from `hackrf_transfer`. Eventually it will be
able to analyze most types of signal files including wav files, etc,
but the first goal is to make something minimally useful for the
HackRF community. 

`wow` is cross-platform: It's was designed from the ground up to run
pretty much anywhere that runs `mono` or Windows.  The effort thus far
has been setting up this stable base on which to build features.
(It's been pretty
[![ResidentSleeper](https://git.io/ResidentSleeper)](//git.io/memes)
so far, but hopefully the UI will be like
[![PogChamp](https://git.io/PogChamp)](//git.io/memes) pretty soon.)

I'd be grateful if you'd try it out, even though it does nothing
useful yet.  If there are any rough spots that feel like
[![BibleThump](https://git.io/BibleThump)](//git.io/memes) then please
let me know

I've noticed that software projects are sometimes extremely difficult
to build from source depending on your platform, so I'm trying to make
"it's a pleasure to build it from source!" a first-class feature from
the very beginning of this project.  Hopefully it'll encourage people
to tinker with the code or submit a pull request.

The current plan is to get a basic spectrogram visualizater up and
running as quickly as possible, choosing to release one tiny feature
at a time.  I intend to follow [GitHub
Flow](https://guides.github.com/introduction/flow/); in particular,
the `master` branch should always contain working code, and feature
additions should be small and frequent.  Release whenever a feature
has been added and hasn't broken anything.

---

### installation

1. install dependencies for your platform:

-

#### windows dependencies

Pop open a beer, 'cause there ain't no thang for you to do but to head
down to step 2.



#### ubuntu / debian dependencies

Paste this into your terminal:

```bash

cat <<'EOF' > wow_such_debian.sh

# update your sources.
sudo apt-get update

# install git and checkout the repo.
sudo apt-get install git -y

# install realpath.
sudo apt-get install realpath -y

# install Mono.  Time to go grab a cup of coffee Kappa
sudo apt-get install mono-complete -y

EOF

# install the dependencies.
bash ./wow_such_debian.sh; ```



#### os x dependencies

Paste this into a Terminal window:

```bash

cat <<'EOF' > wow_such_osx.sh

# install Homebrew. http://brew.sh
if [ -z "`which brew`" ]; then ruby -e "$(curl -fsSL
  https://raw.githubusercontent.com/Homebrew/install/master/install)";
fi

# update Homebrew.
brew update

# install `realpath`
if [ -z "`which realpath`" ]; then brew tap strayptr/tap; brew install
  realpath; fi

# install `git`
if [ -z "`which git`" ]; then brew install git; fi

# install `mono`.
if [ -z "`which mono`" ]; then brew install mono; fi

EOF

# install the dependencies.
bash ./wow_such_osx.sh;

```

-

2. Grab a release archive from
[git.io/wow-many-release](//git.io/wow-many-release), such as
[wow-v0.0.1.tar.gz](https://github.com/strayptr/wow/releases/download/v0.0.1-alpha/wow-0.0.1.tar.gz).

(Alternatively, you can also get releases from
[//git.io/wow-such-release](wow-such-release),
[//git.io/wow-very-releases](wow-very-releases), or
[//git.io/wow-release](wow-release) depending on how you're feeling.)

3. Extract the archive.

4. To launch the visualizer, navigate to `wow-0.0.1/wow/bin` and
double click on `wow` (or `wow.cmd` on Windows).

- (or run `wow-0.0.1/wow/bin/wow` from a terminal window or from
cmd.exe.  Same thing.)

5. (Optional installation step.) To install `wow`:

- **Linux** users: Copy `wow-0.0.1/wow` to `/usr/local/wow` then add
the bin folder to your PATH: `export PATH="$PATH:/usr/local/wow/bin"`.
Now you can run `wow` from anywhere.  Thrilling.

- **Windows** users: Create a shortcut to `wow.cmd` on your desktop.
Name it something like "help im trapped in a shortcut factory".  You
could also add `C:\wow-0.0.1\wow\bin` to your PATH if you want to
launch wow from a cmd.exe terminal (assuming you extracted the archive
to `C:\`).

### build from source

**Install the dependencies** via the "install" section above.  It
includes everything you need to build from source.

#### os x

```bash

cat <<'EOF' > wow_such_osx.sh

#
# Install Homebrew. http://brew.sh
#
if [ -z "`which brew`" ]; then ruby -e "$(curl -fsSL
  https://raw.githubusercontent.com/Homebrew/install/master/install)"
fi

#
# Update Homebrew.
#
brew update

#
# Install `realpath`
#
if [ -z "`which realpath`" ]; then brew tap strayptr/tap brew install
  realpath fi

#
# Install `git`
#
if [ -z "`which git`" ]; then brew install git fi

#
# Install `mono`.
#
# NOTE:  If you get build errors later on, then uninstall your
# existing Mono by e.g. https://discussions.apple.com/thread/3848498
#
if [ -z "`which mono`" ]; then brew install mono fi

EOF

# install the dependencies.
bash ./wow_such_osx.sh; ```

- NOTE:  If you're getting strange build errors, consider
[uninstalling your existing
Mono](https://discussions.apple.com/thread/3848498) and then
installing it via Homebrew.

#### ubuntu / debian

Paste this into your terminal:

```
# get the code.
git clone https://github.com/strayptr/wow && cd wow

# build the project and switch to the build output dir.
etc/make_build.py && cd build/wow-such-signal ```

Then proceed from step 4 of the install directions above. 

You end up with a folder `build/wow-such-signal`.  It's analogous to
the folder you'd get from extracting a release archive, like
`wow-v0.0.1/`.  It's a self-contained directory structure which can be
copied anywhere else and has no external dependencies, i.e. it's a
portable installation.

The process of making an actual release is exactly the same, except
you pass in a version number:

- run `etc/make_build.py --version x.y.z` where x/y/z are
major/minor/revision versions.  This will produce a
`build/wow-x.y.z.tar.gz` file.
  
- create a [GitHub
release](https://github.com/blog/1547-release-your-software) and
attach the `wow-x.y.z.tar.gz` file.



