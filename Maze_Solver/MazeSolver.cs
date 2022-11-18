// Name: Noah Braasch
// Date: October 2 2022

using System;

namespace Maze_Solver
{
    /// <summary>
    /// This class is used for solving a char array maze.
    /// You might want to add other methods to help you out.
    /// A print maze method would be very useful, and probably neccessary to print the solution.
    /// If you are real ambitious, you could make a seperate class to handle that.
    /// </summary>
    class MazeSolver
    {
        /// <summary>
        /// Calls the recursive method maze traversal with the starting information. 
        /// </summary>
        /// <param name="maze">Maze array</param>
        /// <param name="yStart">y starting coordinate</param>
        /// <param name="xStart">x starting coordinate</param>
        /// <param name="remainingOrbs">number of remaining orbs in the maze</param>
        public void SolveMaze(char[,] maze, int yStart, int xStart, int remainingOrbs)
        {
            mazeTraversal(maze, yStart, xStart, ref remainingOrbs);
            // Un comment this draw maze for the large maze
            //DrawMaze(maze);
        }


        /// <summary>
        /// This is the recursive method that solves the maze more detailed description of how it works can be found with line comments
        /// </summary>
        /// <param name="maze">maze array</param>
        /// <param name="yCurrent">current y position</param>
        /// <param name="xCurrent">current x position</param>
        /// <param name="remainingOrbs">number of remaning orbs passed as reference so an accurate count can be accessed while deep in recursion</param>
        /// <returns></returns>
        private bool mazeTraversal(char[,] maze, int yCurrent, int xCurrent, ref int remainingOrbs)
        {
            // Constants that make the code slightly easier to read
            const int UP = 1;
            const int RIGHT = 1;
            const int DOWN = -1;
            const int LEFT = -1;

            // Set current location character based on orb status
            if (remainingOrbs == 0)
                maze[yCurrent, xCurrent] = 'X';
            else
                maze[yCurrent, xCurrent] = '+';

            //Draw the maze on screen after being updated
            // If you want solve the large maze, comment this out
            //DrawMaze(maze);

            // If on the outer bounds of array, return true, because this means the maze is solved
            // 
            if (yCurrent == (maze.GetLength(0) - 1) || xCurrent == (maze.GetLength(1) - 1) || yCurrent == 0 || xCurrent == 0)
            {
                if (remainingOrbs != 0)
                {
                    // Without this line, in the event the exit is found before the orbs are all found, the character wont update.
                    maze[yCurrent, xCurrent] = '-';

                    //Draw the maze on screen after being updated
                    // If you want solve the large maze, comment this out
                    //DrawMaze(maze);
                    return false;
                }
                // This extra draw maze can be un-commented if you want to just have the end output of the maze 
                DrawMaze(maze);
                return true;          
            }

            // If yCurrent + UP is a valid path, or an orb recursivley call. 
            if (IsValidPath(maze[(yCurrent + UP), xCurrent], remainingOrbs))
            {
                // If it is an orb, move there and decrement orb count
                if (maze[(yCurrent + UP), xCurrent] == '@')
                    --remainingOrbs;

                //Recursivley calls while checking so see if it will return true. These true returns allow the cascade back to the termination of the program
                if (mazeTraversal(maze, (yCurrent + UP), xCurrent, ref remainingOrbs) == true)
                    return true;
            }
            // Repeats abve but for the saquare to the right
            if (IsValidPath(maze[yCurrent, (xCurrent + RIGHT)], remainingOrbs))
            {
                if (maze[yCurrent, (xCurrent + RIGHT)] == '@')
                    --remainingOrbs;
                if (mazeTraversal(maze, yCurrent, (xCurrent + RIGHT),ref remainingOrbs) == true)
                    return true;
            }
            // repeats above but for the square to the bottom
            if (IsValidPath(maze[(yCurrent + DOWN), xCurrent], remainingOrbs))
            {
                if (maze[(yCurrent + DOWN), xCurrent] == '@')
                    --remainingOrbs;
                if (mazeTraversal(maze, (yCurrent + DOWN), xCurrent, ref remainingOrbs) == true)
                    return true;
            }
            // repeats above but for the square to the left
            if (IsValidPath(maze[yCurrent, (xCurrent + LEFT)], remainingOrbs))
            {
                if (maze[yCurrent, (xCurrent + LEFT)] == '@')
                    --remainingOrbs;
                if (mazeTraversal(maze, yCurrent, (xCurrent + LEFT), ref remainingOrbs) == true)
                    return true ;
            }
            // If none of the directions returned true, meaning youre at a dead end, then they will cascade back to here and set the current location one-by-one to a - or O until there is another branch that can be taken.
            if (remainingOrbs == 0)
                maze[yCurrent, xCurrent] = 'O';
            else
                maze[yCurrent, xCurrent] = '-';
            // Draw the maze after being updated
            // If you want to solve the large maze comment this out as well
            // DrawMaze(maze);
            return false;
        }
        /// <summary>
        /// Draws the maze on screen with apropriate color coding 
        /// </summary>
        /// <param name="maze">maze to be drawn</param>
        public void DrawMaze(char[,] maze)
        {      
            System.Threading.Thread.Sleep(250);
            // Uncomment if you dont want to see maze history
            // Console.Clear();
            for (int x = 0; x < maze.GetLength(0); ++x)
            {
                for (int y = 0; y < maze.GetLength(1); ++y)
                {
                    if (maze[x, y] == '.')
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    if (maze[x, y] == '+')
                        Console.ForegroundColor = ConsoleColor.Blue;
                    if (maze[x, y] == '-' || maze[x, y] == 'O')
                        Console.ForegroundColor = ConsoleColor.Red;
                    if (maze[x, y] == 'X')
                        Console.ForegroundColor = ConsoleColor.Green;
                    if (maze[x, y] == '@')
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(maze[x, y]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Checks if the character given is a valid path option based on the number of orbs collected
        /// </summary>
        /// <param name="pathChar"></param>
        /// <param name="remainingOrbs"></param>
        /// <returns>the validity of the path</returns>
        private bool IsValidPath(char pathChar, int remainingOrbs)
        {
            if (pathChar == '.' || pathChar == '@' || (remainingOrbs == 0 && (pathChar == '-' || pathChar == '+')))
                return true;
            else
                return false;
        }
    }
}
