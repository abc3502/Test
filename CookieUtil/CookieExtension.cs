using CookieUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 用于方便使用Cookie的扩展工具类
/// </summary>
public static class CookieExtension
{
	// 我们可以为一些使用频率高的类型写专门的【读取】方法

	/// <summary>
	/// 从一个Cookie中读取字符串值。
	/// </summary>
	/// <param name="cookie"></param>
	/// <returns></returns>
	public static string GetString(this HttpCookie cookie)
	{
		if( cookie == null )
			return null;

		return cookie.Value;
	}

	/// <summary>
	/// 从一个Cookie中读取 Int 值。
	/// </summary>
	/// <param name="cookie"></param>
	/// <param name="defaultVal"></param>
	/// <returns></returns>
	public static int ToInt(this HttpCookie cookie, int defaultVal)
	{
		if( cookie == null )
			return defaultVal;

		return Convert.ToInt32(defaultVal);
	}

	/// <summary>
	/// 从一个Cookie中读取值并转成指定的类型
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="cookie"></param>
	/// <returns></returns>
	public static T ConverTo<T>(this HttpCookie cookie)
	{
		if( cookie == null )
			return default(T);

		return (T)Convert.ChangeType(cookie.Value, typeof(T));
	}

	/// <summary>
	/// 从一个Cookie中读取【JSON字符串】值并反序列化成一个对象，用于读取复杂对象
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="cookie"></param>
	/// <returns></returns>
	public static T FromJson<T>(this HttpCookie cookie)
	{
		if( cookie == null )
			return default(T);

		return cookie.Value.ToObject<T>();
	}


	/// <summary>
	/// 将一个对象写入到Cookie
	/// </summary>
	/// <param name="obj"></param>
	/// <param name="name"></param>
	/// <param name="expries"></param>
	public static void WriteCookie(this object obj, string name, DateTime? expries)
	{
		if( obj == null )
			throw new ArgumentNullException("obj");

		if( string.IsNullOrEmpty(name) )
			throw new ArgumentNullException("name");
		

		HttpCookie cookie = new HttpCookie(name, obj.ToString());

		if( expries.HasValue )
			cookie.Expires = expries.Value;

		HttpContext.Current.Response.Cookies.Add(cookie);
	}

	/// <summary>
	/// 删除指定的Cookie
	/// </summary>
	/// <param name="name"></param>
	public static void DeleteCookie(string name)
	{
		if( string.IsNullOrEmpty(name) )
			throw new ArgumentNullException("name");

		HttpCookie cookie = new HttpCookie(name);

		// 删除Cookie，其实就是设置一个【过期的日期】
		cookie.Expires = new DateTime(1900, 1, 1);
		HttpContext.Current.Response.Cookies.Add(cookie);
	}
}



public static class TestClass
{
	public static void Write()
	{
		string str = "中国";
		int aa = 25;
		DisplaySettings setting = new DisplaySettings { Style = 3, Size = 50 };
		DateTime dt = new DateTime(2012, 1, 1, 12, 0, 0);

		str.WriteCookie("Key1", DateTime.Now.AddDays(1d));
		aa.WriteCookie("Key2", null);
		setting.ToJson().WriteCookie("Key3", null);
		dt.WriteCookie("Key4", null);
	}

	public static void Read()
	{
		HttpRequest request = HttpContext.Current.Request;

		string str = request.Cookies["Key1"].GetString();
		int num = request.Cookies["Key2"].ToInt(0);
		DisplaySettings setting = request.Cookies["Key3"].FromJson<DisplaySettings>();
		DateTime dt = request.Cookies["Key4"].ConverTo<DateTime>();
	}	
}



public static class CookieValues
{
	// 建议把Cookie相关的参数放在一起，提供 get / set 属性（或者方法）来访问，以避免"key"到处乱写

	public static string AAA
	{
		get { return HttpContext.Current.Request.Cookies["Key1"].GetString(); }
	}
	public static int BBB
	{
		get { return HttpContext.Current.Request.Cookies["Key2"].ToInt(0); }
	}
	public static DisplaySettings CCC
	{
		get { return HttpContext.Current.Request.Cookies["Key3"].FromJson<DisplaySettings>(); }
	}
	public static DateTime DDD
	{
		get { return HttpContext.Current.Request.Cookies["Key4"].ConverTo<DateTime>(); }
	}
}

