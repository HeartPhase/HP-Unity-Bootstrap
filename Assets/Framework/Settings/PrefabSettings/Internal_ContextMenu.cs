using UnityEditor;
public class Internal_ContextMenu {
[MenuItem("GameObject/Templates/Ball")]
static void CreateBall() {
PrefabUtility.InstantiatePrefab(EditorUtils.CreateFromPrefab(Internal_PrefabEnum.Ball, EditorUtils.NewPrefabMode.BrandNew));}
[MenuItem("GameObject/Templates/BallAndCube")]
static void CreateBallAndCube() {
PrefabUtility.InstantiatePrefab(EditorUtils.CreateFromPrefab(Internal_PrefabEnum.BallAndCube, EditorUtils.NewPrefabMode.BrandNew));}
[MenuItem("GameObject/Templates/Cube")]
static void CreateCube() {
PrefabUtility.InstantiatePrefab(EditorUtils.CreateFromPrefab(Internal_PrefabEnum.Cube, EditorUtils.NewPrefabMode.BrandNew));}
}
