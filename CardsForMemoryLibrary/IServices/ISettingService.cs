namespace CardsForMemoryLibrary.IServices {
    /// <summary>
    ///     配置存储服务
    /// </summary>
    public interface ISettingService {
        object this[string key] { get; set; }
        void delete(string key);
        void clearAll();
    }
}
