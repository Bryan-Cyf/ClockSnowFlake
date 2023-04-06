# ClockSnowFlake


## Nuget包

| Package Name |  Version | Downloads
|--------------|  ------- | ----
| ClockSnowFlake | ![](https://img.shields.io/badge/nuget-v1.0.0-blue) | ![](https://img.shields.io/badge/downloads-xM-brightgreen)|

---------

# `ClockSnowFlake`
> 这是一个基于.NET开源的改进版雪花算法组件，解决了原生雪花算法的时间回拨问题

-------

## 功能介绍
- [x] 支持自定义WorkId
- [x] 基于时间序列，解决时间回拨问题
- [x] 傻瓜式配置，开箱即用
- [x] [关键实现代码](https://github.com/Bryan-Cyf/ClockSnowFlake/blob/master/ClockSnowFlake/src/Tools.SnowFlake/Ids/ClockSnowflakeId.cs)
 ---------

 
# 项目接入

## Step 1 : 安装包，通过Nuget安装包

```powershell
Install-Package ClockSnowFlake
```

## Step 2 : 配置 Startup 启动类

```csharp
public class Startup
{
    //...
    
    public void ConfigureServices(IServiceCollection services)
    {
        //configuration
        services.AddSnowFlakeId(x => x.WorkId = 2);
    }    
}
```

### Step 3 : Id生成器使用

```csharp
[ApiController]
[Route("[controller]/[action]")]
public class SnowFlakeController : ControllerBase
{
    /// <summary>
    /// 获取Id
    /// </summary>
    [HttpGet]
    public long GetId()
    {
        return IdGener.GetLong();
    }
}
```

# 雪花算法的时间回拨问题

## 问题描述

简单说就是时间被调整回到了之前的时间，由于雪花算法重度依赖机器的当前时间，所以一旦发生时间回拨，将有可能导致生成的 ID 可能与此前已经生成的某个 ID 重复（前提是刚好在同一毫秒生成 ID 时序列号也刚好一致），这就是雪花算法最经常讨论的问题——时间回拨

## 现象引发

-  网络时间校准
-  人工设置
-  出现负闰秒

## 原生雪花算法的处理方式

- 直接抛出异常

在雪花算法原本的实现中，针对这种问题，算法本身只是返回错误，由应用另行决定处理逻辑，如果是在一个并发不高或者请求量不大的业务系统中，错误等待或者重试的策略问题不大，但是如果是在一个高并发的系统中，这种策略显得过于粗暴

---------

# 基于时钟序列解决时间回拨的方案

## 简介

> 这里采用的是一种基于修改扩展位的思路，基于时钟序列的雪花算法
> 二进制64位长整型数字：1bit保留 + 41bit时间戳 + 3位时钟序列 + 7bit机器 + 12bit序列号

![](media/content-base64.png?raw=true)
![](media/algorithm.png?raw=true)



## 设计思路

- 如上图，将原本10位的机器码拆分成3位时钟序列及7位机器码
- 发生时间回拨的时候，时间已经发生了变化，那么这时将时钟序列新增1位，重新定义整个雪花Id
- 为了避免实例重启引起时间序列丢失，因此时钟序列最好通过DB/缓存等方式存储起来

## 算法支持

- 还是支持最长 69 年多的运行时间
- 分布式实例规模由2^10（1024）降至2^7（128）
- 单实例每毫秒仍然支持 4096次请求
- 每个分布式实例支持最多 2^3（8） 次时间回拨

## 关键实现代码
- [点击跳转代码位置](https://github.com/Bryan-Cyf/ClockSnowFlake/blob/master/ClockSnowFlake/src/Tools.SnowFlake/Ids/ClockSnowflakeId.cs)

## 更多示例

1. 查看 [使用例子](https://github.com/Bryan-Cyf/ClockSnowFlake/tree/master/sample)
2. 查看 [测试用例](https://github.com/Bryan-Cyf/ClockSnowFlake/tree/master/test)

