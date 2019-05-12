using Windows.Storage;

namespace CardsForMemory.IServices {
    /// <summary>
    ///     配置存储服务
    /// 配置存储服务采用单件模式
    /// 全局位置配置存储服务：SettingService.Instance
    /// </summary>
    public interface ISettingService {
        /// <summary>
        ///     初始化出厂配置
        /// </summary>
        void Init();

        /// <summary>
        ///     存储一项设置
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void Save(string key, string value);

        /// <summary>
        ///     存储一组设置
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="compositeValue">复合值</param>
        void Save(string key, ApplicationDataCompositeValue compositeValue);

        /// <summary>
        ///     获取一项设置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Value(string key);

        /// <summary>
        ///     获取一组设置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        ApplicationDataCompositeValue CompositeValue(string key);

        /// <summary>
        ///     还原出厂设置
        /// </summary>
        void RestoreSettings();
    }
}
