

window.onload = function () {
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var data = JSON.parse(arr.replace(reg, '"'));
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var data2 = JSON.parse(arrID.replace(reg, '"'));
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var dataP = JSON.parse(arrP.replace(reg, '"'));
    if (data.length < 20) {
        $("#more").remove();
    }
    for (var i = 0; i < data.length; i++) {
        var listItem = document.createElement("a");
        if (dataP[i] == 1) {
            listItem.className = "list-group-item";
            listItem.style.backgroundColor = "#DFF0D8"
        } else {
            listItem.className = "list-group-item";
        }
        listItem.innerHTML = data[i];
        contractListItems.appendChild(listItem);

        listItem.href = "/Contract/ContractContent?ID=" + data2[i];
    }
   

    $("#txt").val = "222";

}
var corpId = sessionStorage.getItem("corpId");
var N = 0;
var $txt_keyword="";
var $txt_startDate="";
var $txt_endDate = "";

addLoadEvent(addClickEvent);
addLoadEvent(cha);
addLoadEvent(ex);


function cha() {
    if (ch != "") {
    if (DingTalkPC) {
        DingTalkPC.biz.ding.post({
            users: [sessionStorage.getItem("Project"), sessionStorage.getItem("Production")],
            corpId: corpId, //加密的企业id
            type: 1, //钉类型 1：image  2：link
            alertType: 2,
            alertDate: { },
            attachment: {
                images: [''], //只取第一个image
            }, //附件信息
            text: "合同“"+ch+"”已建立，请跟据实际工作情况及时填写", //消息体
            onSuccess: function () {
                DingTalkPCdevice.notification.alert({
                    message: "发送成功",
                    title: "提示",//可传空
                    buttonName: "确定",
                    onSuccess: function () {

                    },
                    onFail: function (err) { }
                });
            },
            onFail: function () {
                DingTalkPCdevice.notification.alert({
                    message: "发送失败",
                    title: "提示",//可传空
                    buttonName: "确定",
                    onSuccess: function () {

                    },
                    onFail: function (err) { }
                });
            }
        })
    }
    
    if (dd) {
        var url = window.location.href;
        $.post("/DingDing/GetSignPackage?url=" + url, function (data, status) {
            var ddjson = data;
            var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
            var _config = JSON.parse(ddjson.replace(reg, '"'));
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
                
                dd.biz.ding.post({
                    users: [sessionStorage.getItem("Project"), sessionStorage.getItem("Production")],//用户列表，userid
                    corpId: corpId, //加密的企业id
                    type: 0, //附件类型 1：image  2：link
                    alertType: 2,
                    text: '合同“' + ch + '”已建立，请跟据实际工作情况及时填写', //消息体
                    onSuccess: function () {
                        dd.ready(function () {
                            dd.device.notification.alert({
                                message: "OK",
                                title: "提示",//可传空
                                buttonName: "确定",
                                onSuccess: function () {

                                },
                                onFail: function (err) {

                                }
                            });
                        });
                    },
                    onFail: function (err) {
                        var aa = JSON.stringify(err);
                        dd.ready(function () {
                            dd.device.notification.alert({
                                message: "发送失败" ,
                                title: "提示",//可传空
                                buttonName: "确定",
                                onSuccess: function () {

                                },
                                onFail: function (err) { }
                            });
                        });
                    }
                });
            });
            dd.error(function (err) {                                             //验证失败  
                dd.device.notification.alert({
                    message: "尝试退出或刷新" + err,
                    title: "验证失败",//可传空
                    buttonName: "确定",
                    onSuccess: function () {

                    },
                    onFail: function (err) { }
                });
            });
        });

    }
   
}
}
function ex() {
    if (p != "") {
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
}
function addLoadEvent(func) {
	var oldonload = window.onload;
	if(typeof window.onload != 'function') {
		window.onload = func;
	} else {
		window.onload = function() {
			oldonload();
			func();
		}
	}
}

function insertafter(newelement, targetelement) {
	var parent = targetelement.parentnode;
	if(parent.lastchild == targetelement) {
		parent.appendchild(newelement);
	} else {
		parent.insertbefore(newelement, targetelement.nextsibling);
	}
}

//为合同两侧按钮添加事件
function addClickEvent(){
	var contractList = document.getElementById("contractList");
	var btn_addContract = document.getElementsByTagName("span")[0];
	btn_addContract.onclick = function () {
	    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
	    var premission = JSON.parse(UserJson.replace(reg, '"'));
	    if (premission.Summation_p == 0) {
	        if (DingTalkPC) {

	            DingTalkPC.device.notification.alert({
	                message: "抱歉您暂时没有相关权限",
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
	                    message: "抱歉您暂时没有相关权限",
	                    title: "提示",//可传空
	                    buttonName: "确定",
	                    onSuccess: function () {

	                    },
	                    onFail: function (err) { }
	                });
	            });
	        }
	    } else {
	        location.href = "/Contract/AddContract";
	    }
	   
	};
	var btn_refresh = contractList.getElementsByTagName("span")[1];
	btn_refresh.onclick = function () {
	    $("#filter").modal("show");
	};
	var btn_stats = contractList.getElementsByTagName("span")[2];
	btn_stats.onclick = function () {
	    location.href = "/Contract/Stats";
	};
}

//添加假数据
function test(){
    location.href = "/Contract/Index";
}


//往list添加数据
function updateContractList(contractArr,N){
    var contractListItems = document.getElementById("contractListItems");
    var listItems = contractListItems.childNodes;
    //先删除list内所有项
    if(N>20){
	for(var i = listItems.length - 1; i >= 0; i--){
	    contractListItems.removeChild(listItems[i]);
	}}
	//添加项
    for (var i = 0; i < contractArr.length; i++) {
        var listItem = document.createElement("a");
        if (contractArr[i].Process == 1) {
            listItem.className = "list-group-item";
            listItem.style.backgroundColor = "#DFF0D8"
        } else {
            listItem.className = "list-group-item";
        }
		listItem.href = "/Contract/ContractContent?ID=" + contractArr[i].ID;;
		listItem.className = "list-group-item success";
		listItem.innerHTML = contractArr[i].ContractName;
		contractListItems.appendChild(listItem);
	}
}

function lazyLoad() {
   
    if (sessionStorage.getItem("txt_keyword") == null) {
        var $txt_keyword = "";
    } else {
        var $txt_keyword = sessionStorage.getItem("txt_keyword");
    } if (sessionStorage.getItem("txt_startDate") == null && sessionStorage.getItem("txt_endDate") == null) {
        var $txt_startDate = "";
        var $txt_endDate = "";
    } else {
        var $txt_startDate=sessionStorage.getItem("txt_startDate");
        var $txt_endDate = sessionStorage.getItem("txt_endDate");
    }

    N = N + 20;
    $.ajax({
        type: "POST",
        url: "/Contract/filtrationAjax?ID=" + N + "&txt_keyword=" + $txt_keyword + "&txt_startDate=" + $txt_startDate + "&txt_endDate=" + $txt_endDate,
        async: true,
        cache: false,
        success: function (data) {
          var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
            var contractArr = JSON.parse(data.replace(reg, '"'));
            if (contractArr.length < 20) {
                $("#more").remove();
            if (data == "[]") {
                N -= 20;
               
                return false;
            }  
    }
            updateContractList(contractArr);
        },
        error: function () {
            if (DingTalkPC) {

                DingTalkPC.device.notification.alert({
                    message: "请求失败",
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
                        message: "请求失败",
                        title: "提示",//可传空
                        buttonName: "确定",
                        onSuccess: function () {
                         
                        },
                        onFail: function (err) { }
                    });
                });
            }
        }
    });
}

function filterContract() {
    sessionStorage.removeItem("txt_keyword");
    sessionStorage.removeItem("txt_startDate");
    sessionStorage.removeItem("txt_endDate");
    $txt_keyword = $("#txt_keyword").val();
    $txt_startDate = $("#txt_startDate").val();
    $txt_endDate = $("#txt_endDate").val();
    if (($txt_startDate != "" && $txt_endDate == "") || ($txt_startDate == "" && $txt_endDate != "")) {
        if (DingTalkPC) {

            DingTalkPC.device.notification.alert({
                message: "单个日期不能为空",
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
                    message: "单个日期不能为空",
                    title: "提示",//可传空
                    buttonName: "确定",
                    onSuccess: function () {
                        
                    },
                    onFail: function (err) { }
                });
            });
        }

    } else {
        sessionStorage.setItem("txt_keyword", $txt_keyword);
        sessionStorage.setItem("txt_startDate", $txt_startDate);
        sessionStorage.setItem("txt_endDate", $txt_endDate);
        location.href = "/Contract/filtration?txt_keyword=" + $txt_keyword + "&txt_startDate=" + $txt_startDate + "&txt_endDate=" + $txt_endDate;
    }
}

