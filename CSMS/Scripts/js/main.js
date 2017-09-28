

window.onload = function () {
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var data = JSON.parse(arr.replace(reg, '"'));
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var data2 = JSON.parse(arrID.replace(reg, '"'));
    if (data.length < 20) {
        $("#more").remove();
    }
    for (var i = 0; i < data.length; i++) {
        var listItem = document.createElement("a");
       
        listItem.className = "list-group-item";
        listItem.innerHTML = data[i];
        contractListItems.appendChild(listItem);

        listItem.href = "/Contract/ContractContent?ID=" + data2[i];
    }
  

    $("#txt").val = "222";

}
var N = 0;
var $txt_keyword="";
var $txt_startDate="";
var $txt_endDate="";
addLoadEvent(addClickEvent);
addLoadEvent(ex);
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
	btn_addContract.onclick = function(){
	    location.href = "/Contract/AddContract";
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
	for(var i = 0; i < contractArr.length; i++){
		var listItem = document.createElement("a");
		listItem.href = "/Contract/ContractContent?ID=" + contractArr[i].ID;;
		listItem.className = "list-group-item";
		listItem.innerHTML = contractArr[i].ContractName;
		contractListItems.appendChild(listItem);
	}
}

function lazyLoad() {
   
    if (sessionStorage.getItem("txt_keyword") == null) {
        var $txt_keyword = "";
    } else {
        kd=sessionStorage.getItem("txt_keyword");
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
