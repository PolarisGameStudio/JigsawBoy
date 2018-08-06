
public class CheckUtil {
    /// <summary>
    /// 检测是否是数字
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool checkIsNumber(string number)
    {
        int temp;
        return int.TryParse(number, out temp);
    }
}