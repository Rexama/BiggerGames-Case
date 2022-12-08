using System.Collections;
using System.Collections.Generic;
using _Code.Level;
using _Code.Drag;
using _Code.Piece;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Code._Game
{
    public class GameStateManager : MonoBehaviour
    {
        [Header("Level")]
        [SerializeField] private int level;
        
        [Header("Next Level Button")]
        [SerializeField] private GameObject button; 
        
        [Space(10)]
        [Header("Game Handlers")]
        [SerializeField] private LevelDataLoader levelDataLoader;
        [SerializeField] private Board.Board board;
        [SerializeField] private PieceFactory pieceFactory;
        

        private LevelData _levelData;
        private List<Piece.Piece> _pieces;
        private int _snappedPiecesCount = 0;
        

        public void Start()
        {
            DraggableObject.OnSnap += OnPieceSnap;
            DraggableObject.OnUnSnap += OnPieceUnSnap;
            
            SetUpAndStartGame();
        }

        private void SetUpAndStartGame()
        {
            var fileName = "level" + level + ".txt";
            _levelData = levelDataLoader.LoadLevel(fileName);

            SetGameManagerPosition();

            board.PrepareBoard(_levelData.boardSize);

            _pieces = pieceFactory.CreatePieces(_levelData.pieceDataList);
        }

        private void SetGameManagerPosition()
        {
            if (_levelData.boardSize % 2 == 1)
            {
                transform.position = new Vector3(0.5f, 0.5f);
            }
        }

        private void OnWin()
        {
            button.SetActive(true);
        }

        public void OnNextLevel()
        {
            if(SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }

        private void OnPieceSnap()
        {
            _snappedPiecesCount++;  
            if (_snappedPiecesCount == _pieces.Count)
            {
                StartCoroutine("WaitForPhysicsUpdate");
            }
        }

        IEnumerator WaitForPhysicsUpdate()
        {
            yield return new WaitForSeconds(0.05f);
            var contactCount = board.GetGridsContactCount();
            int winContactCount = _levelData.boardSize * _levelData.boardSize * 4;
            
            if (winContactCount == contactCount)
            {
                OnWin();
            }
        }

        private void OnPieceUnSnap()
        {
            _snappedPiecesCount--;
        }
    }
}