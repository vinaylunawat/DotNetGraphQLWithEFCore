namespace Framework.Configuration.Models
{
    using Framework.Configuration;    

    /// <summary>
    /// Defines the <see cref="ApplicationOptions" />.
    /// </summary>
    public class ApplicationOptions : ConfigurationOptions
    {
        /// <summary>
        /// Gets or sets the ConnectionString.
        /// </summary>
        //public string ConnectionString { get; set; }
        public string ConnectionString { get; set; }

        public string CognitoAuthorityURL { get; set; }

        public AmazonSQSConfigurationOptions amazonSQSConfigurationOptions { get; set; }

        /// <summary>
        /// Gets or sets the amazonSQSCongiguration.
        /// </summary>
        public AmazonS3ConfigurationOptions amazons3ConfigurationOptions { get; set; }
    }
}
