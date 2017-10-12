using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Media;
using Android.Webkit;
using Java.IO;
using MySeries.Client.Core.Services;

namespace MySeries.Client.Android.Services
{
    public class AudioRecorderService : IAudioRecorderService
    {
        int RECORDER_BPP = 16;
        int RECORDER_SAMPLERATE;
        ChannelIn RECORDER_CHANNELS = ChannelIn.Stereo;
        Encoding RECORDER_AUDIO_ENCODING = Encoding.Pcm16bit;

        AudioRecord recorder;
        int bufferSize;
        bool isRecording;
        CancellationTokenSource token;

        public void StartRecording()
        {
            var context = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity;
            var audioManager = (AudioManager)context.GetSystemService(Context.AudioService);
            this.RECORDER_SAMPLERATE = Int32.Parse(audioManager.GetProperty(AudioManager.PropertyOutputSampleRate));

            if (this.recorder != null)
            {
                this.recorder.Release();
            }

            this.bufferSize = AudioRecord.GetMinBufferSize(this.RECORDER_SAMPLERATE, ChannelIn.Mono, Encoding.Pcm16bit);
            this.recorder = new AudioRecord(AudioSource.Mic, this.RECORDER_SAMPLERATE, this.RECORDER_CHANNELS, this.RECORDER_AUDIO_ENCODING, this.bufferSize);
            this.recorder.StartRecording();
            this.isRecording = true;

            this.token = new CancellationTokenSource();
            Task.Run(() => WriteAudioDataToFile(), this.token.Token);
        }

        public string StopRecording()
        {
            if (this.recorder != null)
            {
                this.recorder.Stop();
                this.isRecording = false;
                this.token.Cancel();

                this.recorder.Release();
                this.recorder = null;
            }
            var filename = GetFilename();
            CopyWaveFile(GetTempFilename(), filename);

            return filename;
        }

        void WriteAudioDataToFile()
        {
            byte[] data = new byte[this.bufferSize];
            var filename = GetTempFilename();
            FileOutputStream outputStream = null;

            try
            {
                outputStream = new FileOutputStream(filename);
            }
            catch (Exception ex)
            {
                //TODO
            }

            if (outputStream != null)
            {
                List<byte[]> list = new List<byte[]>();
                while (this.isRecording)
                {
                    this.recorder.Read(data, 0, this.bufferSize);
                    try
                    {
                        outputStream.Write(data);

                        list.Add(data);
                    }
                    catch (Exception ex)
                    {
                        //TODO
                    }
                }

                try
                {
                    outputStream.Close();
                }
                catch (Exception ex)
                {
                    //TODO
                }
            }
        }

        void CopyWaveFile(string tempFile, string permanentFile)
        {
            FileInputStream inputStream = null;
            FileOutputStream outputStream = null;
            long totalAudioLength = 0;
            long totalDataLength = totalAudioLength + 36;
            long sampleRate = this.RECORDER_SAMPLERATE;
            int channels = 2;
            long byteRate = this.RECORDER_BPP * this.RECORDER_SAMPLERATE * channels / 8;

            byte[] data = new byte[this.bufferSize];

            try
            {
                inputStream = new FileInputStream(tempFile);
                outputStream = new FileOutputStream(permanentFile);
                totalAudioLength = inputStream.Channel.Size();
                totalDataLength = totalAudioLength + 36;

                WriteWaveFileHeader(outputStream, totalAudioLength, totalDataLength, sampleRate, channels, byteRate);

                while (inputStream.Read(data) != -1)
                {
                    outputStream.Write(data);
                }
                inputStream.Close();
                outputStream.Close();
                DeleteTempFile();
            }
            catch (Exception ex)
            {
                //TODO
            }
        }

        void WriteWaveFileHeader(FileOutputStream outputStream, long audioLength, long dataLength, long sampleRate, int channels, long byteRate)
        {
            byte[] header = new byte[44];

            header[0] = Convert.ToByte('R'); // RIFF/WAVE header
            header[1] = Convert.ToByte('I'); // (byte)'I';
            header[2] = Convert.ToByte('F');
            header[3] = Convert.ToByte('F');
            header[4] = (byte)(dataLength & 0xff);
            header[5] = (byte)((dataLength >> 8) & 0xff);
            header[6] = (byte)((dataLength >> 16) & 0xff);
            header[7] = (byte)((dataLength >> 24) & 0xff);
            header[8] = Convert.ToByte('W');
            header[9] = Convert.ToByte('A');
            header[10] = Convert.ToByte('V');
            header[11] = Convert.ToByte('E');
            header[12] = Convert.ToByte('f');// 'fmt ' chunk
            header[13] = Convert.ToByte('m');
            header[14] = Convert.ToByte('t');
            header[15] = (byte)' ';
            header[16] = 16; // 4 bytes: size of 'fmt ' chunk
            header[17] = 0;
            header[18] = 0;
            header[19] = 0;
            header[20] = 1; // format = 1
            header[21] = 0;
            header[22] = Convert.ToByte(channels);
            header[23] = 0;
            header[24] = (byte)(sampleRate & 0xff);
            header[25] = (byte)((sampleRate >> 8) & 0xff);
            header[26] = (byte)((sampleRate >> 16) & 0xff);
            header[27] = (byte)((sampleRate >> 24) & 0xff);
            header[28] = (byte)(byteRate & 0xff);
            header[29] = (byte)((byteRate >> 8) & 0xff);
            header[30] = (byte)((byteRate >> 16) & 0xff);
            header[31] = (byte)((byteRate >> 24) & 0xff);
            header[32] = (byte)(2 * 16 / 8); // block align
            header[33] = 0;
            header[34] = Convert.ToByte(this.RECORDER_BPP); // bits per sample
            header[35] = 0;
            header[36] = Convert.ToByte('d');
            header[37] = Convert.ToByte('a');
            header[38] = Convert.ToByte('t');
            header[39] = Convert.ToByte('a');
            header[40] = (byte)(audioLength & 0xff);
            header[41] = (byte)((audioLength >> 8) & 0xff);
            header[42] = (byte)((audioLength >> 16) & 0xff);
            header[43] = (byte)((audioLength >> 24) & 0xff);

            outputStream.Write(header, 0, 44);
        }

        string GetTempFilename()
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, "temp.wav");
        }

        string GetFilename()
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, "Todo.wav");
        }

        void DeleteTempFile()
        {
            var file = new Java.IO.File(GetTempFilename());
            file.Delete();
        }
    }
}