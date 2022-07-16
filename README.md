# AR + GPS + Unity

Specs :
Unity Version 2020.3.36f1
Android and Ios target platform

## Unity Standard package : 
**AR Foundation**
**ARCore XR Plugin**
**Input System**
**Text Mesh Pro**
**Unity UI**

## Custom Assets :
**AR + GPS Location by Daniel Fortes**
**AR Foundation Remote by Kyrylo Kuzyk**
**DOTween by Demigiant**

Final MVP :
- App have GPS feature.
- The AR Object spawned by using wgs84 coordinate system sent from server.
- Camera will cast a Ray that can interact with Gameobject in AR environment.
- Standard authentication system
- Chat support features
- Wallet system
- Languange Localization

Check WIP last update on 16 July 2022, [here](https://drive.google.com/file/d/1JYOjn2o64FYqKFmASXCIW0u1OfPPzRAS/view?usp=sharing).

Check latest builds on your Android Phone [here](https://drive.google.com/file/d/1JlNanbv5qfPfd79a8Hosuwfva8V5eEne/view?usp=sharing), make sure your Android OS at leaset Android 7 **(Nougat)**.

## How to Clone the project properly

**1.** Make sure you have Unity 2020.3.36f1 (LTS) or above installed on your Unity Hub. All prior Unity version and 2021 might have some errors when opening the projects.
**2.** Using manifest.json Unity will auto install all required package except costum package. Project can still work without [AR Foundation Remote 2.0](https://assetstore.unity.com/packages/tools/utilities/ar-foundation-remote-2-0-201106) asset. But make sure you have or bought [AR + GPS Location](https://assetstore.unity.com/packages/tools/integration/ar-gps-location-134882) asset, since this asset are the backbone of the project.
**3** DOTween have some minor application for UI, [check Panel.cs](https://github.com/Fitransyah-Portfolio-Unity-3D/AR-GPS-Coin-Catcher-Prototype/blob/master/Assets/_Project/Scripts/Panel.cs).
