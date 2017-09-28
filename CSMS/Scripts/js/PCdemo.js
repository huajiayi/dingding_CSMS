var Result;
var url = window.location.href;
var corpId;

    $.post("/Contract/GetSignPackage?url=" + url, function (data, status) {
        alert("ok")
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
            alert("ok");
            DingTalkPC.runtime.permission.requestAuthCode({                         //获取code码值  
                corpId: corpId,
                onSuccess: function (info) {
                   
                    $.post("/Contract/GetuserId?code=" + info.code, function (data, status) {
                        var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
                        var Userid = JSON.parse(data.replace(reg, '"'));
                        var userid = Userid.userid;
                       
                        $.post("/Contract/Getuser?userid=" + userid, function (data, status) {
                            var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
                            var User = JSON.parse(data.replace(reg, '"'));
                            DingTalkPC.device.notification.alert({
                                message: "您的ID为" + User.userid,
                                title: "亲爱的" + User.name,//可传空
                                buttonName: "收到",
                                onSuccess: function () {
                                    /*回调*/
                                },
                                onFail: function (err) { }
                            });

                        });
                    });
                },
                onFail: function (err) {
                    
                }
            });
        });
        DingTalkPC.error(function (err) {                                             //验证失败  
            DingTalkPC.device.notification.alert({
                message: err,
                title: "提示",//可传空
                buttonName: "收到",
                onSuccess: function () {
                    /*回调*/
                },
                onFail: function (err) { }
            });
        });
    });
