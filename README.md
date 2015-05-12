# wow

`wow` (pronounced "[![Doge](https://git.io/Doge)](//git.io/memes)") is for analyzing radio signals captured by e.g. [a HackRF](https://github.com/mossmann/hackrf).

### license

MIT.  Use the code however you want.  In fact I expect you to build at least N companies with it and then hire me [![Kappa](https://git.io/Kappa)](//git.io/memes)

### okay

Currently it does nothing at all except display a blank form.  Super useful!  On the other hand, it's completely cross-platform. `wow` runs on Windows and anywhere that has `mono`, and the installation process is effortless:

- go to the Releases page: [github.com/strayptr/wow/releases](https://github.com/strayptr/wow/releases)

- download the latest release's binary archive, like [wow-v0.0.1.tar.gz](https://github.com/strayptr/wow/releases/download/v0.0.1-alpha/wow-0.0.1.tar.gz) and extract it.

- launch the visualizer by navigating to `wow-0.0.1/wow/bin` and double clicking on `wow` (or `wow.cmd` on Windows).

- (or run `wow-0.0.01/win/bin/wow` from a terminal or cmd.exe.  Same thing.)

- (Linux): optionally copy `wow-0.0.1/wow` somewhere like `/usr/local` and then add `/usr/local/wow/bin` to your PATH: `export PATH="$PATH:/usr/local/wow/bin"`.  Now you can type `wow` in your terminal.  Thrilling.

- (Windows): optionally create a shortcut to `wow.cmd`

### build

Building from source was designed to be completely effortless.  It's my hope that by making "developer experience" a first-class feature then people will feel encouraged to mess with the code and maybe submit a pull request.

#### os x

`wow` runs fine on OS X.  I just haven't gotten around to writing/testing the build instructions yet.

For now, download a release from the Releases page, extract it, navigate to wow/bin and double click on 'wow' to launch it.  (Or run e.g. `path_to_wow/wow/bin/wow` from a terminal.)

#### ubuntu / debian

```bash
# update your sources.
sudo apt-get update

# install git and checkout the repo.
sudo apt-get install git -y

git clone https://github.com/strayptr/wow
cd wow

# install realpath.
sudo apt-get install realpath -y

# install Mono.  Time to go grab a cup of coffee Kappa
sudo apt-get install mono-complete -y

# build the project.
etc/make_build.py

# run it.
build/wow-such-signal/wow/bin/wow

# alternatively, install it somewhere, add the bin folder to your
# PATH, and then type wow to run it.
cp -r build/wow-such-signal /usr/local/
export PATH="$PATH:/usr/local/wow-such-signal/wow/bin"
wow
```



