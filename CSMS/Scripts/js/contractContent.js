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





addLoadEvent(function () { fullData(ContractName, Customer, Contract_Type, Contract_Amount, Count, Contract_Number, Contract_Date) });
addLoadEvent(function () { fullServices(ss) });


//填充数据
function fullData(contractName, costumerName, contractType, contractAmount, count, contractNumber, signatureDate){
	document.getElementById("lbl_contractName").innerHTML = "合同名称：" + contractName;
	document.getElementById("lbl_costumerName").innerHTML = "合同方客户名称：" + costumerName;
	document.getElementById("lbl_contractType").innerHTML = "合同类别：" + contractType;
	document.getElementById("lbl_contractAmount").innerHTML = "合同金额（人民币元）：" + Changemoney(contractAmount);
	document.getElementById("lbl_count").innerHTML = "数量（套/个）：" + count;
	document.getElementById("lbl_contractNumber").innerHTML = "合同编号：" + contractNumber;
	document.getElementById("lbl_signatureDate").innerHTML = "合同签署日期：" + signatureDate;
	if (Process == 1) {
	    $("#Pct").attr("checked", "checked");
	}
}
function changeProcess() {
    var Process;
    var kkk = document.getElementById("Pct");
    if(kkk.checked) {
        Process = 1
    } else {
        Process = 0
    }
    $.post("changeProcess?Process=" + Process, function (data, status) {
    
     
    });
}
//填充服务款项
function fullServices(ss) {
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
    var serviceArr = JSON.parse(ss.replace(reg, '"'));
	var serviceList = document.getElementById("serviceList");
	for(var i = 1; i <= serviceArr.length; i++){
		var tr = document.createElement("tr");
		var td = document.createElement("td");
		td.innerHTML = serviceArr[i - 1];
		tr.appendChild(td);
		serviceList.appendChild(tr);
		var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象    
		var premission = JSON.parse(UserJson.replace(reg, '"'));
		if (premission.Summation_p == 0) {
		    $("#deleteContract").css("display", "none");
		    $("#editContract").css("display", "none");
		    $("#Pct").css("display", "none");
		    $("#Process2").css("display", "none");
		    
		}
	}
}


function deleteContract() {

        if (DingTalkPC) {

            DingTalkPC.device.notification.confirm({
                message: "确定要删除合同吗",
                title: "提示",//可传空
                buttonLabels: ['是', '否'],
                onSuccess: function (result) { 

                    if (result.buttonIndex == 0) {
                        location.href = "deleteContract";
                    }

                },
                onFail: function (err) { }
            });

        }
        if (dd) {
            dd.ready(function () {
                dd.device.notification.confirm({
                    message: "确定要删除合同吗",
                    title: "提示",//可传空
                    buttonLabels: ['是', '否'],
                    onSuccess: function (result) {

                        if (result.buttonIndex == 0) {
                            location.href = "deleteContract";

                        }
                    
                    },
                    onFail: function (err) { }
                });
            });
        }
    }
function editContract() {
    if (DingTalkPC) {
        DingTalkPC.device.notification.prompt({
            message: "请输入合同金额",
            title: "合同金额修改",
            buttonLabels: ['确定', '取消'],
            onSuccess: function (result) {
                if (result.buttonIndex == 0) {
                    
                    $.post("editContract?Amount=" + result.value, function (data, status) {
                        document.getElementById("lbl_contractAmount").innerHTML = "合同金额（人民币元）：" + result.value;
                        show(data)
                    });
                }
            },
            onFail: function (err) { }
        });
    }
    if (dd) {
        dd.ready(function () {
            dd.device.notification.prompt({
                message: "请输入合同金额",
                title: "合同金额修改",
                buttonLabels: ['确定', '取消'],
                onSuccess: function (result) {
                    if (result.buttonIndex == 0) {
                        $.post("editContract?Amount=" + result.value, function (data, status) {
                            document.getElementById("lbl_contractAmount").innerHTML = "合同金额（人民币元）：" + result.value;
                            show(data)
                        });
                    }
                },
                onFail: function (err) { }
            });
        });
    }
}


