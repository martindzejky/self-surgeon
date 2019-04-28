using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateTilesFromTilemap : MonoBehaviour {
    public GameObject tissue;
    public GameObject bone;
    public GameObject muscle;
    public GameObject skin;
    public GameObject cellFactory;

    private Tilemap tilemap;

    public void Awake() {
        // https://gamedev.stackexchange.com/a/150949
        this.tilemap = this.GetComponent<Tilemap>();
        var bounds = this.tilemap.cellBounds;
        var allDefinedTiles = this.tilemap.GetTilesBlock(bounds);

        this.tilemap.CompressBounds();

        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                var tile = allDefinedTiles[x + y * bounds.size.x];

                var worldTilePosition = this.tilemap.GetCellCenterWorld(new Vector3Int(
                    x + bounds.position.x,
                    y + bounds.position.y,
                    0
                ));

                if (tile != null) {
                    switch (tile.name) {
                        case "Tissue":
                            Instantiate(this.tissue, new Vector3(
                                worldTilePosition.x,
                                worldTilePosition.y,
                                0f
                            ), Quaternion.identity);
                            break;

                        case "Muscle":
                            Instantiate(this.muscle, new Vector3(
                                worldTilePosition.x,
                                worldTilePosition.y,
                                0f
                            ), Quaternion.identity);
                            break;

                        case "Bone":
                            Instantiate(this.bone, new Vector3(
                                worldTilePosition.x,
                                worldTilePosition.y,
                                0f
                            ), Quaternion.identity);
                            break;

                        case "Skin":
                            Instantiate(this.skin, new Vector3(
                                worldTilePosition.x,
                                worldTilePosition.y,
                                0f
                            ), Quaternion.identity);
                            break;

                        case "CellFactory":
                            Instantiate(this.cellFactory, new Vector3(
                                worldTilePosition.x,
                                worldTilePosition.y,
                                0f
                            ), Quaternion.identity);
                            break;

                        case "GoalMuscle":
                            var muscle = Instantiate(this.muscle, new Vector3(
                                worldTilePosition.x,
                                worldTilePosition.y,
                                0f
                            ), Quaternion.identity);
                            muscle.GetComponent<CanGetHurtTile>().isGoal = true;
                            break;

                        default:
                            Debug.LogWarning("Unknown type name " + tile.name);
                            break;
                    }
                }
            }
        } 

        Destroy(this.gameObject);
    }

    private GameObject SpawnTile(GameObject prefab, float x, float y) {
        return Instantiate(
            prefab,
            new Vector3(x, y, 0f),
            Quaternion.identity
        );
    }
}
