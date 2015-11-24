using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {
	public IntVector2 size;

	public MazeCell cellPrefab;

	public MazePassage passagePrefab;
	public MazeWall wallPrefab;

	public float generationStepDelay;

	private MazeCell[,] cells;

	public IntVector2 RandomCoordinates {
		get {
			return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
		}
	}

	public bool ContainsCoordinates(IntVector2 coordinates) {
		return coordinates.x >= 0 && coordinates.x < size.x &&
			coordinates.z >= 0 && coordinates.z < size.z;
	}

	public MazeCell GetCell(IntVector2 coordinates) {
		return cells[coordinates.x, coordinates.z];
	}

	public IEnumerator Generate() {

		WaitForSeconds delay = new WaitForSeconds (generationStepDelay);

		cells = new MazeCell[size.x, size.z];

		List<MazeCell> activeCells = new List<MazeCell>();

		DoFirstGenerationStep (activeCells);

		while (activeCells.Count > 0) {
			DoNextGenerationStep(activeCells);

			yield return delay;
		}

//		for (int x = 0; x < size.x; x++) {
//			for (int z = 0; z < size.z; z++) {
//				CreateCell( new IntVector2(x, z));
//				yield return delay;
//			}
//		}

	}

	private void DoFirstGenerationStep(List<MazeCell> activeCells) {
		activeCells.Add (CreateCell(RandomCoordinates));
	}

	private void DoNextGenerationStep(List<MazeCell> activeCells) {
		//int currentIndex = activeCells.Count - 1;
		int currentIndex = 0;

		MazeCell currentCell = activeCells[currentIndex];

		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt(currentIndex);
			return;
		}

		MazeDirection direction = currentCell.RandomUninitializedDirection;

		if (direction == MazeDirection.Invalid) {
			activeCells.RemoveAt(currentIndex);
			return;
		}

		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2 ();

		if (ContainsCoordinates (coordinates)) {
			MazeCell neighbor = GetCell(coordinates);

			if (neighbor == null) {
				neighbor = CreateCell(coordinates);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add (neighbor);
			}
			else {
				CreateWall(currentCell, neighbor, direction);
				//activeCells.RemoveAt(currentIndex);
			}
		}
		else {
			CreateWall(currentCell, null, direction);
			//activeCells.RemoveAt(currentIndex);
		}
	}

	private MazeCell CreateCell(IntVector2 coordinates) {
		MazeCell newCell = Instantiate (cellPrefab) as MazeCell;
		cells [coordinates.x, coordinates.z] = newCell;

	
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + "," + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3 (coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);

		return newCell;
	}

	private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazePassage passage = Instantiate (passagePrefab) as MazePassage;
		passage.Initialize (cell, otherCell, direction);

		passage = Instantiate (passagePrefab) as MazePassage;
		passage.Initialize (otherCell, cell, direction);
	}

	private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazeWall wall = Instantiate (wallPrefab) as MazeWall;
		wall.Initialize (cell, otherCell, direction);

		if (otherCell != null) {
			wall = Instantiate (wallPrefab) as MazeWall;
			wall.Initialize (otherCell, cell, direction.GetOpposite());
		}

	}
}
