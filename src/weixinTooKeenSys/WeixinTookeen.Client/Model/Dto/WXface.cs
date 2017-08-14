using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeixinTookeen.Client.Model.Dto
{
    public class WXFace
    {
        public static string AddFace(string message)
        {
            return message.Replace("微笑", "/::)").
                                     Replace("伤心", "/::~").
                                     Replace("美女", "/::B").
                                     Replace("发呆", "/::|").
                                     Replace("墨镜", "/:8-)").
                                     Replace("哭", "/::<").
                                     Replace("羞", "/::$").
                                     Replace("哑", "/::X").
                                     Replace("睡", "/::Z").
                                     Replace("哭", "/::’(").
                                     Replace("囧", "/::-|").
                                     Replace("怒", "/::@").
                                     Replace("调皮", "/::P").
                                     Replace("笑", "/::D").
                                     Replace("惊讶", "/::O").
                                     Replace("难过", "/::(").
                                     Replace("酷", "/::+").
                                     Replace("汗", "/:–b").
                                     Replace("抓狂", "/::Q").
                                     Replace("吐", "/::T").
                                     Replace("笑", "/:,@P").
                                     Replace("快乐", "/:,@-D").
                                     Replace("奇", "/::d").
                                     Replace("傲", "/:,@o").
                                     Replace("饿", "/::g").
                                     Replace("累", "/:|-)").
                                     Replace("吓", "/::!").
                                     Replace("汗", "/::L").
                                     Replace("高兴", "/::>").
                                     Replace("闲", "/::,@").
                                     Replace("努力", "/:,@f").
                                     Replace("骂", "/::-S").
                                     Replace("疑问", "/:?").
                                     Replace("秘密", "/:,@x").
                                     Replace("乱", "/:,@@").
                                     Replace("疯", "/::8").
                                     Replace("哀", "/:,@!").
                                     Replace("鬼", "/:!!!").
                                     Replace("打击", "/:xx").
                                     Replace("bye", "/:bye").
                                     Replace("汗", "/:wipe").
                                     Replace("抠", "/:dig").
                                     Replace("鼓掌", "/:handclap").
                                     Replace("糟糕", "/:&-(").
                                     Replace("恶搞", "/:B-)").
                                     Replace("什么", "/:<@").
                                     Replace("累", "/::-O").
                                     Replace("看", "/:>-|").
                                     Replace("难过", "/:P-(").
                                     Replace("坏", "/::X-)").
                                     Replace("亲", "/::*").
                                     Replace("吓", "/:@x").
                                     Replace("可怜", "/:8*").
                                     Replace("刀", "/:pd").
                                     Replace("水果", "/:<W>").
                                     Replace("酒", "/:beer").
                                     Replace("篮球", "/:basketb").
                                     Replace("乒乓", "/:oo").
                                     Replace("咖啡", "/:coffee").
                                     Replace("美食", "/:eat").
                                     Replace("动物", "/:pig").
                                     Replace("鲜花", "/:rose").
                                     Replace("枯", "/:fade").
                                     Replace("唇", "/:showlove").
                                     Replace("爱", "/:heart").
                                     Replace("分手", "/:break").
                                     Replace("生日", "/:cake").
                                     Replace("电", "/:li");
        }
    }
}
