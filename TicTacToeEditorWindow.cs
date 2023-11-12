// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub Organizations: https://github.com/Rimuru-Dev
//
// **************************************************************** //

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace RimuruDev.Internal.Codebase.Editor
{
    public sealed class TicTacToeEditorWindow : EditorWindow
    {
        private const string WindowName = "Tic Tac Toe";
        private const string GameOverTitle = "Game Over :(";
        private const string WinMessage = " Wins!";
        private const string OK = "OK";
        private const int BoardWidth = 100;
        private const int BoardHeight = 100;

        private readonly string[,] Board = new string[3, 3];
        private bool isXTurn = true;

        [MenuItem("RimuruDev Games/" + nameof(WindowName))]
        public static void ShowWindow() =>
            GetWindow<TicTacToeEditorWindow>(WindowName);

        private void OnEnable() =>
            ResetBoard();

        private void OnGUI()
        {
            DrawBoards();

            if (GUILayout.Button("Reset Board"))
                ResetBoard();
        }

        private void DrawBoards()
        {
            for (var i = 0; i < 3; i++)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    for (var j = 0; j < 3; j++)
                    {
                        if (GUILayout.Button(Board[i, j], GUILayout.Width(BoardWidth), GUILayout.Height(BoardHeight)))
                        {
                            if (Board[i, j] == "")
                            {
                                Board[i, j] = isXTurn ? "X" : "O";
                                isXTurn = !isXTurn;

                                CheckForWinner();
                            }
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        private void ResetBoard()
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    Board[i, j] = "";
                }
            }

            isXTurn = true;
        }

        private void CheckForWinner()
        {
            if (Board == null)
                return;

            CheckingLines();

            CheckingColumns();

            CheckingDiagonals();

            CheckingTheDraw();
        }

        private void CheckingLines()
        {
            for (var i = 0; i < 3; i++)
            {
                if (Board[i, 0] == Board[i, 1] && Board[i, 1] == Board[i, 2] && Board[i, 0] != "")
                {
                    EditorUtility.DisplayDialog(GameOverTitle, Board[i, 0] + WinMessage, OK);
                    ResetBoard();
                    return;
                }
            }
        }

        private void CheckingColumns()
        {
            for (var i = 0; i < 3; i++)
            {
                if (Board[0, i] == Board[1, i] && Board[1, i] == Board[2, i] && Board[0, i] != "")
                {
                    EditorUtility.DisplayDialog(GameOverTitle, Board[0, i] + WinMessage, OK);
                    ResetBoard();
                    return;
                }
            }
        }

        private void CheckingDiagonals()
        {
            if (Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2] && Board[0, 0] != "")
            {
                EditorUtility.DisplayDialog(GameOverTitle, Board[0, 0] + WinMessage, OK);
                ResetBoard();
                return;
            }

            if (Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0] && Board[0, 2] != "")
            {
                EditorUtility.DisplayDialog(GameOverTitle, Board[0, 2] + WinMessage, OK);
                ResetBoard();
            }
        }

        private void CheckingTheDraw()
        {
            var isDraw = true;

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (Board[i, j] == "")
                    {
                        isDraw = false;
                        break;
                    }
                }
            }

            if (isDraw)
            {
                EditorUtility.DisplayDialog(GameOverTitle, "It's a Draw!", OK);

                ResetBoard();
            }
        }
    }
}
#endif