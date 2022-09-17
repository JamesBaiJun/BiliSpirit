using FFmpeg.AutoGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unosquare.FFME;
using Unosquare.FFME.Common;

namespace BiliSpirit.Common
{
    public sealed unsafe class FFMEMediaStream : IMediaInputStream
    {
        private readonly CancellationTokenSource _cancelToken = new CancellationTokenSource();

        private Stream BackingStream = new MemoryStream();
        private readonly object ReadLock = new object();
        private readonly byte[] ReadBuffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaInputStream"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public FFMEMediaStream(Stream stream)
        {
            BackingStream = stream;
            CanSeek = true;
            ReadBuffer = new byte[ReadBufferLength];
            StreamUri = new Uri(Scheme); // not use

        }

        /// <summary>
        /// The custom file scheme (URL prefix) including ://
        /// </summary>
        public static string Scheme => "customfile://";

        /// <inheritdoc />
        public Uri StreamUri { get; }

        /// <inheritdoc />
        public bool CanSeek { get; }

        /// <inheritdoc />
        public int ReadBufferLength => 1024 * 16;

        public InputStreamInitializing OnInitializing {get;set;}

        public InputStreamInitialized OnInitialized { get; set; }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_cancelToken != null)
                _cancelToken.Dispose();

            if (BackingStream != null)
                BackingStream.Dispose();
        }

        /// <summary>
        /// Reads from the underlying stream and writes up to <paramref name="targetBufferLength" /> bytes
        /// to the <paramref name="targetBuffer" />. Returns the number of bytes that were written.
        /// </summary>
        /// <param name="opaque">The opaque.</param>
        /// <param name="targetBuffer">The target buffer.</param>
        /// <param name="targetBufferLength">Length of the target buffer.</param>
        /// <returns>
        /// The number of bytes that have been read
        /// </returns>
        public int Read(void* opaque, byte* targetBuffer, int targetBufferLength)
        {
            lock (ReadLock)
            {
                try
                {
                    var readCount = BackingStream.Read(ReadBuffer, 0, ReadBuffer.Length);
                    if (readCount > 0)
                        Marshal.Copy(ReadBuffer, 0, (IntPtr)targetBuffer, readCount);

                    return readCount;
                }
                catch (Exception)
                {
                    return ffmpeg.AVERROR_EOF;
                }
            }
        }

        /// <inheritdoc />
        public long Seek(void* opaque, long offset, int whence)
        {
            lock (ReadLock)
            {
                try
                {
                    return whence == ffmpeg.AVSEEK_SIZE ?
                        BackingStream.Length : BackingStream.Seek(offset, SeekOrigin.Begin);
                }
                catch
                {
                    return ffmpeg.AVERROR_EOF;
                }
            }
        }
    }
}
