RUL_Unity
===

Version 1.0.0  
This library is designed to simplify randomization and the creation of procedurally generated content. RUL is capable of generating 
pseudo-random numbers, vectors, colors and noise, as well as randomly modifying existing objects.

Rul is split into four modules :
Rul,
RulVec (for vector randomization), 
RulCol (for color randomization) and
RulNoise (for creating noise) .
All these modules are implemented as static classes in the RUL namespace.

Other versions
---------------------
This version of RUL uses Unity's vector and color types. A port for MonoGame, as well as a standalone version exist.

Setting up RUL
----------------------
The easiest way to get started with RUL is to check out the tutorials in the [wiki](https://github.com/CaptainBubbles/RUL/wiki). They cover everything from downloading the source to generating Perlin noise.

Code samples : 
----------------------
    using RUL;
    ...
    long myLong = Rul.RandLong(700,1000); // Creates random long between 700 and 1000

    long myInt = Rul.RandInt(5,10,InclusionOptions.Upper); //Returns random int between 6 and 10

    string name = Rul.RandElement("Jon","Ned","Bran"); //Returns one of the given elements

    int probablyOne = Rul.RandElement(new int[] {1,2,3},0.9F); //Returns 1 in nine out of ten cases

    Vector3 unitVec = RulVec.RandUnitVector3(); //Returns a random 3D vector with length 1
    
    Color lightColor = RulCol.RandColor(0.8F); //Returns a light color
    
    Color darkRed = RulCol.RandColor(Hues.Red, LuminosityTypes.Dark); //Returns a dark shade of red

    float[,] noise = RulNoise.RandSimplexNoise2(400,400); //Returns simplex noise 

License
-----------
This library is published under the very permissive MIT license. See [http://opensource.org/licenses/MIT](http://opensource.org/licenses/MIT) for information on what you can and cannot do with this software.
