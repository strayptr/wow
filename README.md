
### [git.io/wow](https://git.io/wow) - signal visualizer

`wow` (pronounced "[![Doge](https://git.io/Doge)](//git.io/memes)") is
a tool for viewing and analyzing radio signals captured by SDR platforms such as
the [HackRF](https://github.com/mossmann/hackrf) via
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
whatsoever except display a blank form.  (Though if you happen to live
in a faraday cage, this visualization might not be entirely
inaccurate.)

Eventually you'll be able to analyze signal files by running `wow
some-signal-recording.iq`, where `some-signal-recording.iq` is filled
with quadrature samples from `hackrf_transfer`. Someday it might be
able to analyze most types of signal files including wav files, etc,
but the first goal is to produce something minimally useful for the
HackRF community. 

`wow` is cross-platform.  It was designed from the ground up to run
pretty much anywhere that runs `mono` or Windows.  I'm very interested
in getting it working on BSD, but I have no experience with BSD# yet.
It'd be pretty sweet if anyone could help me figure out this part, but
unfortunately I have nothing to reward you with except some dry humor,
an honorary mention in an exclusive "Credits" section (so exclusive it
doesn't even exist yet), and my eternal gratitude (which sadly isn't
edible.)

**I'd be very grateful** if you'd try out `wow` and verify it runs on
your platform, in spite of the fact that it doesn't do anything useful
yet.  If there are any pain points during the install process, please
let me know.  (See 'installation' section below.)  Also let me know if
it's not completely painless to build it from source.  (See 'build'
section below.)

I've often noticed that some software projects can be extremely tricky
to build from source, depending on your platform, so I'm trying to
make "it's a pleasure to build this from source!" a first-class
feature from the very beginning of this project.  Maybe it'll
encourage people to tinker with the code or submit a pull request.

The current plan is to get a basic spectrogram visualizater up and
running as quickly as possible, choosing to release one tiny feature
at a time in rapid succession.  I intend to follow [GitHub
Flow](https://guides.github.com/introduction/flow/); in particular,
the `master` branch should always contain working code, and feature
additions should be small and frequent.  Release whenever a feature
has been added and it hasn't broken anything.

---

### installation step 1 - preparing for your `wow`

Install the dependencies for your platform:

#### windows dependencies

Pop open a beer, 'cause there ain't no thang for you to do but to head
down to installation step 2.  Scroll down like it's 1968!


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

# install build dependencies.
sudo apt-get install build-essential libc6-dev-i386 -y

EOF

# install the dependencies.
bash ./wow_such_debian.sh;

```



#### os x dependencies

Paste this into a Terminal window:

```bash

cat <<'EOF' > wow_such_osx.sh

# sudo privs will be required later for brew cask.
sudo echo 

# install Homebrew. http://brew.sh
if [ -z "`which brew`" ]; then
  ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)";
fi

# update Homebrew.
brew update

# install `realpath`
if [ -z "`which realpath`" ]; then
  brew tap strayptr/tap
  brew install realpath
fi

# install `git`
if [ -z "`which git`" ]; then brew install git; fi

# install `mono`.
if [ -z "`which mono`" ]; then
  # we also need libgdiplus, which doesn't come with `brew install
  # mono`.  See https://github.com/Homebrew/homebrew/pull/34973
  # Let's install mono from `brew cask` instead.

  # install cask, the brew binary package manager.
  brew install caskroom/cask/brew-cask

  # install the full mono-mdk from cask.
  brew cask install mono-mdk
fi

EOF

# install the dependencies.
bash ./wow_such_osx.sh;

```

-

### installation step 2 - choosing your `wow`

Go to the Releases page
([https://git.io/wow-many-release](//git.io/wow-many-release)) and
download the latest archive, such as
[wow-v0.0.1.tar.gz](https://github.com/strayptr/wow/releases/download/v0.0.1-alpha/wow-0.0.1.tar.gz).

- Extract the archive, which produces a folder structure like
`wow-0.0.1/wow/...`

(Note: If https://git.io/wow-many-release doesn't suit your tastes, a
number of alternatives have been prepared):
- [https://git.io/wow-such-release](//git.io/wow-such-release)
- [https://git.io/wow-very-releases](//git.io/wow-very-releases)
- [https://git.io/wow-releases](//git.io/wow-releases)

### installation step 3 - exercising your `wow`

- To launch the visualizer, navigate to `wow-0.0.1/wow/bin` and double
click on `wow` (or `wow.cmd` on Windows).

(Or run `wow-0.0.1/wow/bin/wow` in a terminal window or a cmd.exe
window.  Same thing.)

### installation step 4 (optional) - housing your `wow`

The folder you got from the archive file is portable, i.e. `bin/wow`
will probably run properly no matter where you put it or how you
launch it.  So feel free to put it wherever you want; just add the
subfolder `wow/bin` to your PATH.

Eventually there will probably be some snazzy installer with snazzy
installer features, but in the meantime here's some boilerplate
recipes.

To install `wow`:

**Linux** users: Copy `wow-x.y.z/wow` to `/usr/local/wow` then add the
bin folder to your PATH: `export PATH="$PATH:/usr/local/wow/bin"`.
Now you can run `wow` from anywhere.  Thrilling.

**Windows** users: Create a shortcut to `wow.cmd` on your desktop.
Name it something like `help im trapped in a shortcut factory`.  You
could also add `C:\wow-0.0.1\wow\bin` to your PATH if you want to
launch wow from a cmd.exe terminal (assuming you extracted the release
archive to `C:\`).

### build from source

**Install the dependencies** via **installation step 1** above.  It
includes everything you need to build from source.

Paste this into your terminal:

```bash

# get the code.
git clone https://github.com/strayptr/wow && cd wow

# build the project, then switch to the output folder it generated.
etc/make_build.py && cd build/wow-such-signal

```

If there were no build errors, you'll end up inside a folder path like
  `build/wow-such-signal`.  The `wow-such-signal` folder is analogous
  to the folder you'd get from extracting a release archive, like
  `wow-v0.0.1/`.  It's a self-contained directory structure which can
  be copied anywhere else and has no external dependencies, i.e. it's
  a portable installation.

Really, I'm making it sound more complicated than it is: just run
`wow/bin/wow` in your terminal and the visualizer should launch.

Now you can **goto installation step 3** above and pretend like it's
the folder you got from a release archive.

--

The process of making an actual release is exactly the same, except
you pass in a version number:

- run `etc/make_build.py --version x.y.z` where x/y/z are
major/minor/revision versions.  This will produce a
`build/wow-x.y.z.tar.gz` file.
  
- create a [GitHub
release](https://github.com/blog/1547-release-your-software) and
attach the `wow-x.y.z.tar.gz` file.

--

This README is probably way too long and wordy, but, with apologies,
at this point I think I should stop trying to write READMEs / build
methodologies and start writing useful signal analysis code.

--

Pull requests welcome!  I'd be like
[![PogChamp](https://git.io/PogChamp)](//git.io/memes)


### mindmap

We use [XMind](https://www.xmind.net/) to keep track of notes,
ideas, design decisions, cornercase troubleshooting issues, etc.

Current mindmap:

![Mindmap](http://i.imgur.com/ZBz2lyn.jpg)

