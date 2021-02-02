using UnityEngine;

public class SkinHelper
{
	public static Skin[] LoadSkins(Sprite[] sprites)
	{
		Skin[] skins = new Skin[sprites.Length];

		skins[0] = new Skin(sprites[0], -0.415f, 0.255f, -0.560f, 0.46f);
		skins[1] = new Skin(sprites[1], -0.380f, 0.315f, -0.590f, 0.55f);
		skins[2] = new Skin(sprites[2], -0.395f, 0.285f, -0.560f, 0.53f);
		skins[3] = new Skin(sprites[3], -0.420f, 0.280f, -0.540f, 0.56f);
		skins[4] = new Skin(sprites[4], -0.470f, 0.360f, -0.465f, 0.48f);

		return skins;
	}
}
