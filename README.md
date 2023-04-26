<h1 align="center">Merge2 Game Template</h1>
<p align="center"><i>Unity package. Uses the https://github.com/ArtemVetik/com.agava.merge2.git package and implements the game template in Unity UI.</i></p>

<p align="center">
  <img src="https://img.shields.io/github/license/ArtemVetik/com.agava.merge2uiview" />
  <img src="https://img.shields.io/github/repo-size/ArtemVetik/com.agava.merge2uiview" />
  <img src="https://img.shields.io/github/issues/ArtemVetik/com.agava.merge2uiview" />
  <img src="https://img.shields.io/github/v/release/ArtemVetik/com.agava.merge2uiview?include_prereleases" />
  <a href="https://openupm.com/packages/com.agava.merge2uiview/"><img src="https://img.shields.io/npm/v/com.agava.merge2uiview?label=openupm&registry_uri=https://package.openupm.com" /></a>
</p>

---
## Installation
### Dependency Resolution
This package has dependencies on [com.agava.merge](https://github.com/ArtemVetik/com.agava.merge2.git) and [com.yellowsquad.assetpath](https://github.com/mdlka/com.yellowsquad.assetpath.git). Merge the snippet to **Packages/manifest.json** or add it manually from **Edit/Project Settings/Package Manager**
```
  "scopedRegistries": [
        {
            "name": "package.openupm.com",
            "url": "https://package.openupm.com",
            "scopes": [
                "com.agava.merge2",
                "com.yellowsquad.assetpath"
            ]
        }
    ]
```
### Install from Unity Package Manager
Make sure you have standalone [Git](https://git-scm.com/downloads) installed first. Reboot after installation.  
In Unity, open "Window" -> "Package Manager".  
Click the "+" sign on top left corner -> "Add package from git URL..."  
Paste this: `https://github.com/ArtemVetik/com.agava.merge2uiview.git#3.0.2`  
See minimum required Unity version in the `package.json` file.

This project can also be installed through OpenUPM, [here.](https://openupm.com/packages/com.agava.merge2uiview/)

### Where to go next?
* [Wiki](https://github.com/ArtemVetik/com.agava.merge2uiview/wiki)

## Authors

- [@ArtemVetik](https://www.github.com/ArtemVetik)
- [@mdlka](https://www.github.com/mdlka)
