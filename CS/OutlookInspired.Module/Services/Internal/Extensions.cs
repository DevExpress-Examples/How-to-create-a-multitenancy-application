using System.Drawing;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.MultiTenancy;
using DevExpress.ExpressApp.Utils;
using Microsoft.Data.Sqlite;

namespace OutlookInspired.Module.Services.Internal{
    internal static class Extensions{
    static readonly string DataPath = new DirectoryInfo(Directory.GetCurrentDirectory()).FindFolderInPathUpwards("Data");
    
    internal static string GetTenantConnectionString(this ITenantProvider tenantProvider, string connectionString){
        
        using var connection = new SqliteConnection($"Data source={Path.GetFullPath($"{DataPath}\\{Path.GetFileName(connectionString)}")}");
        var query = "SELECT ConnectionString FROM Tenant WHERE ID = @Id";
        using var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@Id", tenantProvider.TenantId);
        connection.Open();
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            return reader.GetString(0);
        } 
        throw new InvalidOperationException("Tenant not found. Make sure you have already login as super admin at least once.");
    }
        
    public static void AttachDatabase(this IServiceProvider serviceProvider, string connectionString){
        
        if (!File.Exists($"{DataPath}\\OutlookInspired.db")){
            ZipFile.ExtractToDirectory($"{DataPath}\\OutlookInspired.zip",DataPath);
        }
        var dbPath = $"{DataPath}\\{Path.GetFileName(connectionString)}";
        if (!File.Exists(dbPath)){
            File.Copy($"{DataPath}\\OutlookInspired.db",dbPath);
        }
    }
    
    [Obsolete]
    public static Color ColorFromHex(this string hex){
        hex = hex.Replace("#", "");
        return Color.FromArgb(hex.Substring(0, 2).ToByte( 16), hex.Substring(2, 2).ToByte( 16), hex.Substring(4, 2).ToByte(16));
    }
    public static string FindFolderInPathUpwards(this DirectoryInfo current, string folderName){
        var directory = current;
        while (directory.Parent != null){
            if (directory.GetDirectories(folderName).Any()){
                return Path.GetRelativePath(current.FullName, Path.Combine(directory.FullName, folderName));
            }
            directory = directory.Parent;
        }
        throw new DirectoryNotFoundException($"Folder '{folderName}' not found up the tree from '{current.FullName}'");
    }

    public static byte ToByte(this string value,int fromBase) 
        => Convert.ToByte(value, fromBase);
    
    public static CriteriaOperator Combine(this CriteriaOperator criteriaOperator,string criteria,GroupOperatorType type=GroupOperatorType.And){
        var @operator = CriteriaOperator.Parse(criteria);
        return !criteriaOperator.ReferenceEquals(null) ? new GroupOperator(type, @operator, criteriaOperator) : @operator;
    }

    public static string ToBase64String(this byte[] bytes) 
        => Convert.ToBase64String(bytes);
    

    [Obsolete]
    public static string ToBase64Image(this byte[] bytes) 
        => $"data:{bytes.FileType()};base64,{bytes?.ToBase64String()}";
    
    private static bool IsMaskMatch(this byte[] byteArray, int offset, params byte[] mask) 
        => byteArray != null && byteArray.Length >= offset + mask.Length &&
           !mask.Where((t, i) => byteArray[offset + i] != t).Any();

    [Obsolete]
    public static string FileType(this byte[] value) 
        => value switch{
            { Length: > 0 } when value.IsMaskMatch( 0, 77, 77) || value.IsMaskMatch(0, 73, 73) => "tiff",
            { Length: > 0 } when value.IsMaskMatch(1, 80, 78, 71) => "png",
            { Length: > 0 } when value.IsMaskMatch(0, 71, 73, 70, 56) => "gif",
            { Length: > 0 } when value.IsMaskMatch( 0, 255, 216) => "jpeg",
            { Length: > 0 } when value.IsMaskMatch(0, 66, 77) => "bmp",
            _ => ""
        };
    
    public static string GetString(this byte[] bytes, Encoding encoding = null) 
        => bytes == null ? null : (encoding ?? Encoding.UTF8).GetString(bytes);
    
    public static byte[] Bytes(this Stream stream){
        if (stream is MemoryStream memoryStream){
            return memoryStream.ToArray();
        }

        using var ms = new MemoryStream();
        stream.CopyTo(ms);
        return ms.ToArray();
    }

    public static decimal RoundNumber(this decimal d, int decimals = 0) 
        => Math.Round(d, decimals);
    
    [Obsolete]
    public static ImageInfo ImageInfo(this Enum @enum) 
        => ImageLoader.Instance.GetEnumValueImageInfo(@enum);
    [Obsolete]
    public static string ImageName(this Enum @enum) 
        => ImageLoader.Instance.GetEnumValueImageName(@enum);
    
    public static async Task SaveToFileAsync(this Stream stream, string filePath,bool append=false) {
        var directory = Path.GetDirectoryName(filePath) + "";
        if (!Directory.Exists(directory)) {
            Directory.CreateDirectory(directory);
        }

        await using var fileStream = File.OpenWrite(filePath);
        if (!append){
            fileStream.SetLength(0);    
        }
        
        await stream.CopyToAsync(fileStream);
    }
    [Obsolete]
    public static string FirstCharacterToLower(this string str) =>
        string.IsNullOrEmpty(str) || char.IsLower(str, 0) ? str : char.ToLowerInvariant(str[0]) + str.Substring(1);
    
    public static Stream GetManifestResourceStream(this Assembly assembly, Func<string, bool> nameMatch)
        => assembly.GetManifestResourceStream(assembly.GetManifestResourceNames().First(nameMatch));

    public static byte[] Bytes(this string s, Encoding encoding = null) 
        => s == null ? [] : (encoding ?? Encoding.UTF8).GetBytes(s);

    
    public static string MemberExpressionName<TObject, TMemberValue>(this Expression<Func<TObject, TMemberValue>> memberName) 
        => memberName.Body switch{
            MemberExpression memberExpression => memberExpression.Member.Name,
            UnaryExpression{ Operand: MemberExpression operand } => operand.Member.Name,
            _ => throw new ArgumentException("Invalid expression type", nameof(memberName))
        };

    }
}