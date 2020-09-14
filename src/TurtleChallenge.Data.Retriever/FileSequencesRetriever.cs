using System;
using System.IO;
using TurtleChallenge.Data.Retriever.Dto;

namespace TurtleChallenge.Data.Retriever
{
    public class FileSequencesRetriever : ISequencesRetriever, IDisposable
    {
        private StreamReader _movesStreamReader = null;
        private readonly TurtleGameFileSettings _settings;

        public FileSequencesRetriever(TurtleGameFileSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            Init();
        }

        private void Init()
        {
            string movesFilePath = _settings.MovesFile;
            if (File.Exists(movesFilePath))
            {
                try
                {
                    var fileStream = File.OpenRead(movesFilePath);
                    _movesStreamReader = new StreamReader(fileStream);
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to read moves file.");

                    throw;
                }
            }
            else
            {
                Console.WriteLine("Moves file not found.");
            }
        }

        public char[] GetNextSequenceOfMoves()
        {
            return _movesStreamReader?.ReadLine()?.Trim().ToCharArray();
        }

        #region Dispose
        private bool _isDisposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                _movesStreamReader?.Dispose();
            }

            _isDisposed = true;
        }
        #endregion
    }
}
