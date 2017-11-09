var Result;
var url = window.location.href;
var corpId;
$(function () {
    if (p != "") {
        show(p);
    }
})
if (dd) {
    $.post("/DingDing/GetSignPackage?url=" + url, function (data, status) {

        var ddjson = data;
        var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
        var _config = JSON.parse(ddjson.replace(reg, '"'));
        sessionStorage.setItem("corpId", _config.corpId);
        corpId = _config.corpId;
        dd.config(
             {
                 agentId: _config.agentId,
                 corpId: _config.corpId,
                 timeStamp: _config.timeStamp,
                 nonceStr: _config.nonceStr,
                 signature: _config.signature,
                 jsApiList: [
                      'runtime.info',
                      'biz.contact.choose',
                      'device.notification.confirm',
                      'device.notification.alert',
                      'device.notification.prompt',
                      'biz.ding.post',
                      'biz.util.openLink']
             });

        dd.ready(function () {
            //验证成功  
            dd.runtime.permission.requestAuthCode({                         //获取code码值  
                corpId: corpId,
                onSuccess: function (info) {

                    $.post("/DingDing/GetuserId?code=" + info.code, function (data, status) {
                        var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
                        var Userid = JSON.parse(data.replace(reg, '"'));
                        var userid = Userid.userid;

                        $.post("/DingDing/Getuser?userid=" + userid, function (data, status) {
                            var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
                            var Mjson = JSON.parse(data.replace(reg, '"'));
                            var User = JSON.parse(Mjson[0].replace(reg, '"'));
                            var flag = JSON.parse(Mjson[1].replace(reg, '"'));
                            if (flag == "1") {
                                dd.device.notification.alert({
                                    message: "你好" + User.name + ",您的ID为：" + User.userid,
                                    title: "提示",//可传空
                                    buttonName: "确定",
                                    onSuccess: function () {
                                        location.href = "/Contract/Index";
                                    },
                                    onFail: function (err) {
                                    }
                                });
                                location.href = "/Contract/Index";
                            } if (flag == "0") {
                                var a = 1;
                                dd.device.notification.alert({
                                    message: "你好" + User.name + ",您的ID为：" + User.userid,
                                    title: "提示",//可传空
                                    buttonName: "确定",
                                    onSuccess: function () {
                                        a = 1;
                                    },
                                    onFail: function (err) {
                                    }
                                });
                                if(a==1){
                                $(".loader").toggle();
                                $(".vertical-center").toggle();
                                }
                            }
                        });
                    });
                },
                onFail: function (err) {
                    dd.device.notification.alert({
                        message: "你好" + User.name + ",您的ID为：" + User.userid,
                        title: "提示",//可传空
                        buttonName: "确定",
                        onSuccess: function () {
                            $(".loader").toggle();
                            $(".vertical-center").toggle();
                        },
                        onFail: function (err) {
                        }
                    });
                }
            });
        });
       
        dd.error(function (err) {                                             //验证失败  
            $.post("/DingDing/DeleteCache", function (data, status) {
                dd.device.notification.alert({
                    message: "尝试退出或刷新",
                    title: "验证失败",//可传空
                    buttonName: "确定",
                    onSuccess: function () {
                        /*回调*/

                    },
                    onFail: function (err) { }
                });
            })//验证失败  
        });
    });

}
if (DingTalkPC) {
    $.post("/DingDing/GetSignPackage?url=" + url, function (data, status) {
        var ddjson = data;
        var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
        var _config = JSON.parse(ddjson.replace(reg, '"'));
        corpId = _config.corpId;

        DingTalkPC.config(
             {
                 agentId: _config.agentId,
                 corpId: _config.corpId,
                 timeStamp: _config.timeStamp,
                 nonceStr: _config.nonceStr,
                 signature: _config.signature,
                 jsApiList: [
                                   'runtime.info',
                                   'biz.contact.choose',
                                   'device.notification.confirm',
                                   'device.notification.alert',
                                   'device.notification.prompt',
                                   'biz.ding.post',
                                   'biz.util.openLink']
             });

        DingTalkPC.ready(function () {

            DingTalkPC.runtime.permission.requestAuthCode({                         //获取code码值  
                corpId: corpId,
                onSuccess: function (info) {

                    $.post("/DingDing/GetuserId?code=" + info.code, function (data, status) {
                        var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
                        var Userid = JSON.parse(data.replace(reg, '"'));
                        var userid = Userid.userid;
                        alert(data);
                        $.post("/DingDing/Getuser?userid=" + userid, function (data, status) {
                           
                            var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象

                            var Mjson = JSON.parse(data.replace(reg, '"'));
                            var User = JSON.parse(Mjson[0].replace(reg, '"'));
                            var flag = JSON.parse(Mjson[1].replace(reg, '"'));
                            if (flag == "1") {
                                DingTalkPC.device.notification.alert({
                                    message: "你好" + User.name + ",您的ID为：" + User.userid,
                                    title: "提示",//可传空
                                    buttonName: "确定",
                                    onSuccess: function () {
                                        location.href = "/Contract/Index";
                                    },
                                    onFail: function (err) { }
                                });
                            } if (flag == "0") {
                                DingTalkPC.device.notification.alert({
                                    message: "你好" + User.name + ",您的ID为：" + User.userid,
                                    title: "提示",//可传空
                                    buttonName: "确定",
                                    onSuccess: function () {
                                        $(".loader").toggle();
                                        $(".vertical-center").toggle();
                                    },
                                    onFail: function (err) { }
                                });

                            }
                        });
                    });
                },
                onFail: function (err) {

                }
            });
        });
        DingTalkPC.error(function (err) {                                             //验证失败  
            $.post("/DingDing/DeleteCache", function (data, status) {
                DingTalkPC.device.notification.alert({
                    message: "尝试退出或刷新",
                    title: "验证失败",//可传空
                    buttonName: "确定",
                    onSuccess: function () {
                        /*回调*/

                    },
                    onFail: function (err) { }
                });
            })//验证失败  
        });
    });

}
function show(p) {
    if (DingTalkPC) {

        DingTalkPC.device.notification.alert({
            message: p,
            title: "提示",//可传空
            buttonName: "确定",
            onSuccess: function () {
                /*回调*/

            },
            onFail: function (err) { }
        });

    }
    if (dd) {
        dd.ready(function () {
            dd.device.notification.alert({
                message: p,
                title: "提示",//可传空
                buttonName: "确定",
                onSuccess: function () {

                },
                onFail: function (err) { }
            });
        });
    }
}