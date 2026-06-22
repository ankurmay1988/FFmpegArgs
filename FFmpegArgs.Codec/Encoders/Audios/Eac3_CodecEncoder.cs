/*Encoder eac3 [ATSC A/52 E-AC-3]:
    General capabilities: dr1
    Threading capabilities: none
    Supported sample rates: 48000 44100 32000
    Supported sample formats: fltp
    Supported channel layouts: mono stereo 3.0(back) 3.0 quad(side) quad 4.0 5.0(side) 5.0 2 channels (FC+LFE) 2.1 4 channels (FL+FR+LFE+BC) 3.1 4.1 5.1(side) 5.1
E-AC-3 Encoder AVOptions:
  -mixing_level      <int>        E...A...... Mixing Level (from -1 to 111) (default -1)
  -room_type         <int>        E...A...... Room Type (from -1 to 2) (default -1)
     notindicated    0            E...A...... Not Indicated (default)
     large           1            E...A...... Large Room
     small           2            E...A...... Small Room
  -per_frame_metadata <boolean>    E...A...... Allow Changing Metadata Per-Frame (default false)
  -copyright         <int>        E...A...... Copyright Bit (from -1 to 1) (default -1)
  -dialnorm          <int>        E...A...... Dialogue Level (dB) (from -31 to -1) (default -31)
  -dsur_mode         <int>        E...A...... Dolby Surround Mode (from -1 to 2) (default -1)
     notindicated    0            E...A...... Not Indicated (default)
     on              2            E...A...... Dolby Surround Encoded
     off             1            E...A...... Not Dolby Surround Encoded
  -original          <int>        E...A...... Original Bit Stream (from -1 to 1) (default -1)
  -dmix_mode         <int>        E...A...... Preferred Stereo Downmix Mode (from -1 to 3) (default -1)
     notindicated    0            E...A...... Not Indicated (default)
     ltrt            1            E...A...... Lt/Rt Downmix Preferred
     loro            2            E...A...... Lo/Ro Downmix Preferred
     dplii           3            E...A...... Dolby Pro Logic II Downmix Preferred
  -ltrt_cmixlev      <float>      E...A...... Lt/Rt Center Mix Level (from -1 to 2) (default -1)
  -ltrt_surmixlev    <float>      E...A...... Lt/Rt Surround Mix Level (from -1 to 2) (default -1)
  -loro_cmixlev      <float>      E...A...... Lo/Ro Center Mix Level (from -1 to 2) (default -1)
  -loro_surmixlev    <float>      E...A...... Lo/Ro Surround Mix Level (from -1 to 2) (default -1)
  -dsurex_mode       <int>        E...A...... Dolby Surround EX Mode (from -1 to 3) (default -1)
     notindicated    0            E...A...... Not Indicated (default)
     on              2            E...A...... Dolby Surround EX Encoded
     off             1            E...A...... Not Dolby Surround EX Encoded
     dpliiz          3            E...A...... Dolby Pro Logic IIz-encoded
  -dheadphone_mode   <int>        E...A...... Dolby Headphone Mode (from -1 to 2) (default -1)
     notindicated    0            E...A...... Not Indicated (default)
     on              2            E...A...... Dolby Headphone Encoded
     off             1            E...A...... Not Dolby Headphone Encoded
  -ad_conv_type      <int>        E...A...... A/D Converter Type (from -1 to 1) (default -1)
     standard        0            E...A...... Standard (default)
     hdcd            1            E...A...... HDCD
  -stereo_rematrixing <boolean>    E...A...... Stereo Rematrixing (default true)
  -channel_coupling  <int>        E...A...... Channel Coupling (from -1 to 1) (default auto)
     auto            -1           E...A...... Selected by the Encoder
  -cpl_start_band    <int>        E...A...... Coupling Start Band (from -1 to 15) (default auto)
     auto            -1           E...A...... Selected by the Encoder
*/
namespace FFmpegArgs.Codec.Encoders.Audios
{
    /// <summary>
    /// ATSC A/52 E-AC-3 (Enhanced AC-3).<br/>
    /// Enumerated options reuse the shared AC-3 enums (<see cref="Ac3_RoomType"/>, <see cref="Ac3_DsurMode"/>, etc.) since the value tables are identical.
    /// </summary>
    public class Eac3_CodecEncoder : BaseAudioCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        public Eac3_CodecEncoder(AudioOutputAVStream stream) : base("eac3", stream)
        {
        }

        /// <summary>
        /// Mixing Level (from -1 to 111) (default -1)
        /// </summary>
        public Eac3_CodecEncoder MixingLevel(int mixing_level)
            => this.SetOptionRange("-mixing_level", mixing_level, -1, 111);

        /// <summary>
        /// Room Type (from -1 to 2) (default -1)
        /// </summary>
        public Eac3_CodecEncoder RoomType(Ac3_RoomType room_type)
            => this.SetOptionRange("-room_type", (int)room_type, -1, 2);

        /// <summary>
        /// Allow Changing Metadata Per-Frame (default false)
        /// </summary>
        public Eac3_CodecEncoder PerFrameMetadata(bool per_frame_metadata)
            => this.SetOption("-per_frame_metadata", per_frame_metadata.ToFFmpegFlag());

        /// <summary>
        /// Copyright Bit (from -1 to 1) (default -1)
        /// </summary>
        public Eac3_CodecEncoder Copyright(int copyright)
            => this.SetOptionRange("-copyright", copyright, -1, 1);

        /// <summary>
        /// Dialogue Level (dB) (from -31 to -1) (default -31)
        /// </summary>
        public Eac3_CodecEncoder Dialnorm(int dialnorm)
            => this.SetOptionRange("-dialnorm", dialnorm, -31, -1);

        /// <summary>
        /// Dolby Surround Mode (from -1 to 2) (default -1)
        /// </summary>
        public Eac3_CodecEncoder DsurMode(Ac3_DsurMode dsur_mode)
            => this.SetOptionRange("-dsur_mode", (int)dsur_mode, -1, 2);

        /// <summary>
        /// Original Bit Stream (from -1 to 1) (default -1)
        /// </summary>
        public Eac3_CodecEncoder Original(int original)
            => this.SetOptionRange("-original", original, -1, 1);

        /// <summary>
        /// Preferred Stereo Downmix Mode (from -1 to 3) (default -1)
        /// </summary>
        public Eac3_CodecEncoder DmixMode(Ac3_DmixMode dmix_mode)
            => this.SetOptionRange("-dmix_mode", (int)dmix_mode, -1, 3);

        /// <summary>
        /// Lt/Rt Center Mix Level (from -1 to 2) (default -1)
        /// </summary>
        public Eac3_CodecEncoder LtrtCmixlev(float ltrt_cmixlev)
            => this.SetOptionRange("-ltrt_cmixlev", ltrt_cmixlev, -1, 2);

        /// <summary>
        /// Lt/Rt Surround Mix Level (from -1 to 2) (default -1)
        /// </summary>
        public Eac3_CodecEncoder LtrtSurmixlev(float ltrt_surmixlev)
            => this.SetOptionRange("-ltrt_surmixlev", ltrt_surmixlev, -1, 2);

        /// <summary>
        /// Lo/Ro Center Mix Level (from -1 to 2) (default -1)
        /// </summary>
        public Eac3_CodecEncoder LoroCmixlev(float loro_cmixlev)
            => this.SetOptionRange("-loro_cmixlev", loro_cmixlev, -1, 2);

        /// <summary>
        /// Lo/Ro Surround Mix Level (from -1 to 2) (default -1)
        /// </summary>
        public Eac3_CodecEncoder LoroSurmixlev(float loro_surmixlev)
            => this.SetOptionRange("-loro_surmixlev", loro_surmixlev, -1, 2);

        /// <summary>
        /// Dolby Surround EX Mode (from -1 to 3) (default -1)
        /// </summary>
        public Eac3_CodecEncoder DsurexMode(Ac3_DsurexMode dsurex_mode)
            => this.SetOptionRange("-dsurex_mode", (int)dsurex_mode, -1, 3);

        /// <summary>
        /// Dolby Headphone Mode (from -1 to 2) (default -1)
        /// </summary>
        public Eac3_CodecEncoder DheadphoneMode(Ac3_DheadphoneMode dheadphone_mode)
            => this.SetOptionRange("-dheadphone_mode", (int)dheadphone_mode, -1, 2);

        /// <summary>
        /// A/D Converter Type (from -1 to 1) (default -1)
        /// </summary>
        public Eac3_CodecEncoder AdConvType(Ac3_AdConvType ad_conv_type)
            => this.SetOptionRange("-ad_conv_type", (int)ad_conv_type, -1, 1);

        /// <summary>
        /// Stereo Rematrixing (default true)
        /// </summary>
        public Eac3_CodecEncoder StereoRematrixing(bool stereo_rematrixing)
            => this.SetOption("-stereo_rematrixing", stereo_rematrixing.ToFFmpegFlag());

        /// <summary>
        /// Channel Coupling (from -1 to 1) (default auto, -1 = Selected by the Encoder)
        /// </summary>
        public Eac3_CodecEncoder ChannelCoupling(int channel_coupling)
            => this.SetOptionRange("-channel_coupling", channel_coupling, -1, 1);

        /// <summary>
        /// Coupling Start Band (from -1 to 15) (default auto, -1 = Selected by the Encoder)
        /// </summary>
        public Eac3_CodecEncoder CplStartBand(int cpl_start_band)
            => this.SetOptionRange("-cpl_start_band", cpl_start_band, -1, 15);
    }

    public static partial class AudioEncoderExtensions
    {
        /// <summary>
        /// ATSC A/52 E-AC-3 (Enhanced AC-3) encoder.
        /// </summary>
        public static Eac3_CodecEncoder Eac3_Codec(this AudioOutputAVStream stream)
            => new Eac3_CodecEncoder(stream);
        /// <summary>
        /// ATSC A/52 E-AC-3 (Enhanced AC-3) encoder.
        /// </summary>
        public static T Eac3_Codec<T>(this T stream, Action<Eac3_CodecEncoder> action) where T : AudioOutputAVStream
        {
            action.Invoke(stream.Eac3_Codec());
            return stream;
        }
    }
}
