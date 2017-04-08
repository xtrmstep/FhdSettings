namespace SettingsService.Core.Data.Models
{
    /// <summary>
    ///     Type of crawled data
    /// </summary>
    public enum ExtratorDataType
    {
        Link = 1, // this should start from 1, because if it starts from 0 all Link become NULL when mapped with AutoMapper
        Video,
        Picture
    }
}