//#pragma strict

//var terrain:Terrain;
//var tData:TerrainData;
//var xRes:int;
//var yRes:int;
//
//var heights:float[];
//
//function Start () {
//	terrain = transform.GetComponent(Terrain);
//	tData = terrain.terrainData;
//
//	xRes = tData.heightmapWidth;
//	yRes = tData.heightmapHeight;
//	terrain.activeTerrain.heightmapMaximumLOD = 0;
//}

//function OnGUI() {
//	if (GUI.Button(Rect(10, 10, 100, 25), "Wrinkle")) {
		//randomizePoints(0.1);
	//}
	//if (GUI.Button(Rect(10, 40, 100, 25), "Reset")) {
//		resetPoints();
//	}
//}
//
//function randomizePoints(strength:float) {
//	var heights = tData.GetHeights(0, 0, xRes, yRes);
//	for(y = 0; y < yRes; ++y) {
		//for(x = 0; x < xRes; ++x) {
//			heights[x, y] = Random.Range(0.0, strength)*0.5;
		//}
	//}
	//tData.SetHeights(0, 0, heights);
//}
//
//function resetPoints() {
//	var heights = tData.GetHeights(0, 0, xRes, yRes);
//	for(y = 0; y < yRes; ++y) {
		//for(x = 0; x < xRes; ++x) {
//			heights[x, y] = 0;
		//}
	//}
	//tData.SetHeights(0, 0, heights);
//}
//
//function Update () {
//	
//}
//////