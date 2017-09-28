
var Result;
var url = window.location.href;
var corpId;

if (dd) {
    $.post("/Contract/GetSignPackage?url=" + url, function (data, status) {

        var ddjson = data;
        var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
        var _config = JSON.parse(ddjson.replace(reg, '"'));
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

                    $.post("/Contract/GetuserId?code=" + info.code, function (data, status) {
                        var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
                        var Userid = JSON.parse(data.replace(reg, '"'));
                        var userid = Userid.userid;

                        $.post("/Contract/Getuser?userid=" + userid, function (data, status) {
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
                            } if (flag == "0") {
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
                },
                onFail: function (err) {

                }
            });
        });
        dd.error(function (err) {                                             //验证失败  
            dd.device.notification.alert({
                message: "尝试退出或刷新" +  err,
                title: "验证失败",//可传空
                buttonName: "确定",
                onSuccess: function () {

                },
                onFail: function (err) { }
            });
        });
    });

}
if (DingTalkPC) {
    $.post("/Contract/GetSignPackage?url=" + url, function (data, status) {

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

                    $.post("/Contract/GetuserId?code=" + info.code, function (data, status) {
                        var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
                        var Userid = JSON.parse(data.replace(reg, '"'));
                        var userid = Userid.userid;

                        $.post("/Contract/Getuser?userid=" + userid, function (data, status) {
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
            DingTalkPC.device.notification.alert({
                message: "尝试退出或刷新" + err,
                title: "验证失败",//可传空
                buttonName: "确定",
                onSuccess: function () {
                    /*回调*/
                },
                onFail: function (err) { }
            });
        });
    });

}