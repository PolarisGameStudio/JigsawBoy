

public class SteamUserInfoParams : BaseParams
{
    /// <summary>
    ///steamIds 用逗号分割
    /// </summary>
    public string steamids { get; set; }

    /// <summary>
    /// 社区群组KEY
    /// </summary>
    public string key { get; set; }

    /// <summary>
    /// Appid
    /// </summary>
    public string appid { get; set; }
}