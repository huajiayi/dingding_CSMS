var a = 1;
$(function () {
    $("#filterIcon").click(function () {
        $("#div_accumulatedValue").slideUp("fast");
        $("#filter").modal("show");
        a=1
    });
    $("#filterIcon2").click(function () {
        $("#div_accumulatedValue").slideDown("fast");
        $("#filter").modal("show");
        a=2
    });
    $("#select_statsMode").change(function () {
        if ($(this).val() == "按合同类型") {
            $("#div_statsType").slideDown("fast");
        } else {
            $("#div_statsType").slideUp("fast");
        }
    });
    //$("#select_statsType").change(function () {
    //    if ($(this).val() == "累计值") {
    //        $("#div_accumulatedValue").slideDown("fast");
    //    } else {
    //        $("#div_accumulatedValue").slideUp("fast");
    //    }
    //});
    var minDate = "2015-01-01"; //最小合同签署日期
    var minYear = new Date(minDate).getFullYear();
    var thisYear = new Date().getFullYear();
    for (var i = minYear; i <= thisYear; i++) {
        $("#select_year").append("<option value='" + i + "'>" + i + "</option>");
    }
    var today = new Date().Format("yyyy-MM-dd");
    $("#btn_all").click(function () {
        $("#txt_startDate").val(minDate);
        $("#txt_finishDate").val(today);
    });
    $("#btn_all").click();
    $("#select_year").change(function () {
        if ($(this).val() != "") {
            $("#txt_startDate").val($(this).val() + "-01-01");
            $("#txt_finishDate").val($(this).val() + "-12-31");
        }
    });
    $("#btn_thisYear").click(function () {
        $("#txt_startDate").val(thisYear + "-01-01");
        $("#txt_finishDate").val(thisYear + "-12-31");
    });
    var thisQuarterStartDate = new Date(thisYear, getQuarterStartMonth(), 1).Format("yyyy-MM-dd");
    var thisQuarterEndDate = new Date(thisYear, getQuarterStartMonth() + 3, 0).Format("yyyy-MM-dd");
    $("#btn_thisQuarter").click(function () {
        $("#txt_startDate").val(thisQuarterStartDate);
        $("#txt_finishDate").val(thisQuarterEndDate);
    });
    var thisMonth = new Date().getMonth() + 1;
    var thisMonthStartDate = new Date(thisYear + "/" + thisMonth + "/1").Format("yyyy-MM-dd");
    var thisMonthEndDate = new Date(thisYear, thisMonth, 0).Format("yyyy-MM-dd");
    $("#btn_thisMonth").click(function () {
        $("#txt_startDate").val(thisMonthStartDate);
        $("#txt_finishDate").val(thisMonthEndDate);
    });
});

function filterStats() {
    var start = $("#txt_startDate").val();
    var end = $("#txt_finishDate").val();
    var typ = $("#select_contractType").val();

    if (a == 2 && $("#div_statsType").is(":hidden")) {
        $.post("/Contract/DoStats?Start=" + start + "&End=" + end + "&Typ=", function (result, status) {


            //salesChart.setOption({
            //    title: {
            //        text: "合同收款情况",
            //        subtext: "总金额：" + (result[0] + result[3])
            //    },
            //    series: [{
            //        data: [{
            //            value: result[3],
            //            name: "未收金额"
            //        },
            //        {
            //            value: result[0],
            //            name: "已收金额"
            //        }
            //        ]
            //    }],
            //});
            Arr.setOption({
                title: {
                    text: "合同收款情况",
                },
                series: [{
                    data: [{
                        value: result[0],
                        name: "确认收入金额"
                    },
                    {
                        value: result[1],
                        name: "签署金额"
                    },
                    {
                        value: result[2],
                        name: "未收金额"
                    },
                    {
                        value: result[3],
                        name: "已收金额"
                    }

                    ]
                }],
            });
            Arr2.setOption({
                title: {
                    text: "生产发货情况",
                },
                series: [{
                    data: [{
                        value: result[4],
                        name: arr2[0],
                    },
                    {
                        value: result[5],
                        name: arr2[1],
                    },
                    {
                        value: result[6],
                        name: arr2[2],
                    },
                    {
                        value: result[7],
                        name: arr2[3],
                    },
                     {
                         value: result[8],
                         name: arr2[4],
                     },
                     {
                         value: result[9],
                         name: arr2[5],
                     }
                    ]
                }],
            });
        }, "json");
    } else if (a == 2 && $("#div_statsType").is(":visible")) {
        $.post("/Contract/DoStats?Start=" + start + "&End=" + end + "&Typ=" + typ, function (result, status) {

            //salesChart.setOption({
            //    title: {
            //        text: "所有合同收款情况",
            //        subtext: "总金额：" + (result[0] + result[3])
            //    },
            //    series: [{
            //        data: [{
            //            value: result[3],
            //            name: "未收金额"
            //        },
            //        {
            //            value: result[0],
            //            name: "已收金额"
            //        }
            //        ]
            //    }],
            //});
            Arr.setOption({
                title: {
                    text: "合同收款情况",
                },
                series: [{
                    data: [{
                        value: result[0],
                        name: "确认收入金额"
                    },
                    {
                        value: result[1],
                        name: "签署金额"
                    },
                    {
                        value: result[2],
                        name: "未收金额"
                    },
                    {
                        value: result[3],
                        name: "已收金额"
                    }

                    ]
                }],
            });
            Arr2.setOption({
                title: {
                    text: "生产发货情况",
                },
                series: [{
                    data: [{
                        value: result[4],
                        name: arr2[0],
                    },
                    {
                        value: result[5],
                        name: arr2[1],
                    },
                    {
                        value: result[6],
                        name: arr2[2],
                    },
                    {
                        value: result[7],
                        name: arr2[3],
                    },
                     {
                         value: result[8],
                         name: arr2[4],
                     },
                     {
                         value: result[9],
                         name: arr2[5],
                     }
                    ]
                }],
            });
        }, "json");
    }  if (a==1&& $("#div_statsType").is(":hidden")) {
        accumulatedValueChart.setOption({
            title: {
                text: "签署金额",
            },
            series: [{
                data: ToArray(aYear_on_yearGrowthRate1)
            },
				{
				    data: ToArray(aLinkGrowthRate1)
				}
            ]
        });
        AffirmIncomeAmountChart.setOption({
            title: {
                text: "已收金额",
            },
            series: [{
                data: ToArray(aYear_on_yearGrowthRate3)
            },
				{
				    data: ToArray(aLinkGrowthRate3)
				}
            ]
        });
        InvoiceAmountChart.setOption({
            title: {
                text: "确认收入金额",
            },
            series: [{
                data: ToArray(aYear_on_yearGrowthRate2)
            },
				{
				    data: ToArray(aLinkGrowthRate2)
				}
            ]
        });
        NoAmountCollections.setOption({
            title: {
                text: "确认收入金额",
            },
            series: [{
                data: ToArray(aYear_on_yearGrowthRate4)
            },
				{
				    data: ToArray(aLinkGrowthRate4)
				}
            ]
        });
        console.log(1);
    } if (a == 1 && $("#div_statsType").is(":visible")) {
        accumulatedValueChart.setOption({
            title: {
                text:"数据加载请稍后...",
            }
        });
        AffirmIncomeAmountChart.setOption({
            title: {
                text: "数据加载请稍后...",
            }
        });
        InvoiceAmountChart.setOption({
            title: {
                text: "数据加载请稍后...",
            }
        });
        NoAmountCollections.setOption({
            title: {
                text: "数据加载请稍后...",
            }
        });
        $.post("/Contract/ChangeStats?Type=" + typ, function (result, status) {

            accumulatedValueChart.setOption({
                title: {
                    text: $("#select_contractType").val() + "签署金额",
                },
                series: [{
                    data: result[0]
                },
                    {
                        data: result[1]
                    }
                ]
            });
            AffirmIncomeAmountChart.setOption({
                title: {
                    text: $("#select_contractType").val() + "已收金额",
                },
                series: [{
                    data: result[4]
                },
                    {
                        data: result[5]
                    }
                ]
            });
            InvoiceAmountChart.setOption({
                title: {
                    text: $("#select_contractType").val() + "确认收入金额",
                },
                series: [{
                    data: result[2]
                },
                    {
                        data: result[3]
                    }
                ]
            });
            NoAmountCollections.setOption({
                title: {
                    text: $("#select_contractType").val() + "未收入金额",
                },
                series: [{
                    data: result[6]
                },
                    {
                        data: result[7]
                    }
                ]
            });
        }, "json");
    }
    $("#filter").modal("hide");
}

//$(".content .glyphicon").click(function(){
//	$("#filter").modal("toggle");
//});
function ToArray(Json) {
    var reg = new RegExp("&quot;", "g"); //创建正则RegExp对象     
    var data2 = JSON.parse(Json.replace(reg, '"'));
    return data2;
}

var accumulatedValueChart;
var InvoiceAmountChart;
var NoAmountCollections
var Arr
var Arr2
var AffirmIncomeAmountChart;
var salesChart;
var salesBarChart;
var producitonChart;
var producitonBarChart;
var warehouseChart;
var warehouseBarChart;
var arr = ["确认收入金额", "签署金额", "未收入金额", "已收入金额"];
var arr2 = ["未生产量", "已生产量", "未发货量", "库存量", "已发货量","需求量"];
$(function () {
    accumulatedValueChart = initLineChart($("#accumulatedValueChart")[0], "签署金额", ToArray(aYear_on_yearGrowthRate1), ToArray(aLinkGrowthRate1));
    InvoiceAmountChart = initLineChart($("#InvoiceAmountChart")[0], "确认收入金额", ToArray(aYear_on_yearGrowthRate2), ToArray(aLinkGrowthRate2));
    AffirmIncomeAmountChart = initLineChart($("#AffirmIncomeAmountChart")[0], "已收金额", ToArray(aYear_on_yearGrowthRate3), ToArray(aLinkGrowthRate3));
    NoAmountCollections = initLineChart($("#NoAmountCollection")[0], "未收金额", ToArray(aYear_on_yearGrowthRate4), ToArray(aLinkGrowthRate4));
    //salesChart = initChart($("#salesChart")[0], "收款情况", "总金额：￥", "未收金额", "已收金额", NoTotalRevenue, TotalRevenue);
    //salesBarChart = initBarChart($("#salesBarChart")[0], "各合同收款情况", "未收金额", "已收金额", ToArray(ContractName), ToArray(NoAmountCollection), ToArray(SubAffirmIncomeAmount));
    Arr = initBarChart1($("#Arr")[0], "合同收款情况","金额", arr, ToArray(Ar));
    Arr2 = initBarChart2($("#Arr2")[0], "生产发货情况","数量", arr2, ToArray(Ar2));
    //producitonChart = initChart($("#producitonChart")[0], "生产情况", "需求量：", "未生产量", "已生产量", SumNoTotalProduct, SumTotalProduct);
    //producitonBarChart = initBarChart($("#producitonBarChart")[0], "各合同生产情况", "未生产量", "已生产量", ToArray(ContractName), ToArray(NoTotalProduct), ToArray(TotalProduct));
    //warehouseChart = initChart($("#warehouseChart")[0], "发货情况", "库存量：" + SumReserves, "未发货数量", "已发货数量", SumNoShippedCount, SumShippedCount);
    //warehouseBarChart = initBarChart($("#warehouseBarChart")[0], "各合同发货情况", "未发货数量", "已发货数量", ToArray(ContractName), ToArray(NoShippedCount), ToArray(ShippedCount));
    window.addEventListener("resize", function () {
        accumulatedValueChart.resize();
        Arr.resize();
        InvoiceAmountChart.resize();
        AffirmIncomeAmountChart.resize();
        NoAmountCollections.resize();
        //salesChart.resize();
        //salesBarChart.resize();
        //producitonChart.resize();
        //producitonBarChart.resize();
        //warehouseChart.resize();
        //warehouseBarChart.resize();
    });
});

function initChart(element, titleText, titleSubtext, legendData1, legendData2, dataValue1, dataValue2) {
    var myChart = echarts.init(element);
    var _subtext = titleSubtext + (dataValue1 + dataValue2);
    if (titleText == "发货情况") {
        _subtext = titleSubtext;
    }
    var option = {
        title: {
            text: titleText,
            subtext: _subtext,
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{b} : ￥{c} ({d}%)"
        },
        legend: {
            orient: 'vertical',
            left: 'left',
            data: [legendData1, legendData2]
        },
        series: [{
            name: "contractIncome",
            type: 'pie',
            radius: '55%',
            center: ['50%', '60%'],
            data: [{
                value: dataValue1,
                name: legendData1
            },
				{
				    value: dataValue2,
				    name: legendData2
				}
            ],
            itemStyle: {
                emphasis: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                }
            }
        }]
    };
    myChart.setOption(option, false, true);
    return myChart;
}

function initBarChart(element, titleText, legendData1, legendData2, aContractName, dataValue1, dataValue2, hasDataZoom) {
    var myBarChart = echarts.init(element);
    var option = {
        title: {
            text: titleText,
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} : ￥{c}"
        },
        legend: {
            orient: 'vertical',
            left: 'left',
            data: [legendData1, legendData2],
        },
        grid: {
            bottom: '30%'
        },
        xAxis: {
            data: aContractName,
            axisLabel: {
                interval: 0, //横轴信息全部显示  
                rotate: -30, //-30度角倾斜显示  
            }
        },
        yAxis: {},
        dataZoom: [{ // 这个dataZoom组件，默认控制x轴。
            type: 'slider', // 这个 dataZoom 组件是 slider 型 dataZoom 组件
            start: 0, // 左边在 10% 的位置。
            end: 10, // 右边在 60% 的位置。
            //				filterMode: 'empty'
        },
			{ // 这个dataZoom组件，也控制x轴。
			    type: 'inside', // 这个 dataZoom 组件是 inside 型 dataZoom 组件
			    start: 0, // 左边在 10% 的位置。
			    end: 10 // 右边在 60% 的位置。
			}
        ],
        series: [{
            name: legendData2,
            type: 'bar',
            stack: 'barChart',
            data: dataValue2,
            itemStyle: {
                normal: {
                    color: '#333'
                }
            }
        },
			{
			    name: legendData1,
			    type: 'bar',
			    stack: 'barChart',
			    data: dataValue1
			}
        ]
    };
    myBarChart.setOption(option, false, true);
    return myBarChart;
}

function initBarChart2(element, titleText, legendData1, aContractName, dataValue1) {
    var myBarChart = echarts.init(element);
    var option = {
        title: {
            text: titleText,
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} : {c}（个/套）"
        },
        legend: {
            orient: 'vertical',
            left: 'left',
            data: [legendData1],
        },
        grid: {
            bottom: '30%'
        },
        xAxis: {
            data: aContractName,
            axisLabel: {
                interval: 0, //横轴信息全部显示  
                rotate: -30, //-30度角倾斜显示  
            }
        },
        yAxis: {},
       
        series: [{
            name: legendData1,
            type: 'bar',
            data: dataValue1,
            itemStyle: {
                normal: {
                    color: '#333'
                }
            }
        }
        ]
    };
    myBarChart.setOption(option, false, true);
    return myBarChart;
}
function initBarChart1(element, titleText, legendData1, aContractName, dataValue1) {
    var myBarChart = echarts.init(element);
    var option = {
        title: {
            text: titleText,
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} : ￥{c}"
        },
        legend: {
            orient: 'vertical',
            left: 'left',
            data: [legendData1],
        },
        grid: {
            bottom: '30%'
        },
        xAxis: {
            data: aContractName,
            axisLabel: {
                interval: 0, //横轴信息全部显示  
                rotate: -30, //-30度角倾斜显示  
            }
        },
        yAxis: {},

        series: [{
            name: legendData1,
            type: 'bar',
            data: dataValue1,
            itemStyle: {
                normal: {
                    color: '#333'
                }
            }
        }
        ]
    };
    myBarChart.setOption(option, false, true);
    return myBarChart;
}
function initLineChart(element, titleText, dataValue1, dataValue2) {
    var myLineChart = echarts.init(element);
    option = {
        title: {
            text: titleText,
            x: 'center'
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            orient: 'vertical',
            left: 'left',
            data: ['同比增长率', '环比增长率']
        },
        toolbox: {
            show: true,
        },
        calculable: true,
        xAxis: [{
            type: 'category',
            boundaryGap: false,
            data: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
            axisLabel: {
                interval: 0
            }
        }],
        yAxis: [{
            type: 'value',
        }],
        dataZoom: [{ // 这个dataZoom组件，默认控制x轴。
            type: 'slider', // 这个 dataZoom 组件是 slider 型 dataZoom 组件
            start: 0, // 左边在 10% 的位置。
            end: 50, // 右边在 10% 的位置。
            //				filterMode: 'empty'
        },
			{ // 这个dataZoom组件，也控制x轴。
			    type: 'inside', // 这个 dataZoom 组件是 inside 型 dataZoom 组件
			    start: 0, // 左边在 10% 的位置。
			    end: 50 // 右边在 10% 的位置。
			}
        ],
        series: [{
            name: '同比增长率',
            type: 'line',
            data: dataValue1
        },
			{
			    name: '环比增长率',
			    type: 'line',
			    data: dataValue2
			}
        ]
    };
    myLineChart.setOption(option, false, true);
    return myLineChart;
}

Date.prototype.Format = function (fmt) { //author: meizz   
    var o = {
        "M+": this.getMonth() + 1, //月份   
        "d+": this.getDate(), //日   
        "h+": this.getHours(), //小时   
        "m+": this.getMinutes(), //分   
        "s+": this.getSeconds(), //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds() //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

function back() {
    window.history.back(-1);
    //if (sessionStorage.getItem("txt_keyword") == null) {
    //    var $txt_keyword = "";
    //} else {
    //    var $txt_keyword = sessionStorage.getItem("txt_keyword");
    //} if (sessionStorage.getItem("txt_startDate") == null && sessionStorage.getItem("txt_endDate") == null) {
    //    var $txt_startDate = "";
    //    var $txt_endDate = "";
    //} else {
    //    var $txt_startDate = sessionStorage.getItem("txt_startDate");
    //    var $txt_endDate = sessionStorage.getItem("txt_endDate");
    //}
    //location.href = "/Contract/filtration?txt_keyword=" + $txt_keyword + "&txt_startDate=" + $txt_startDate + "&txt_endDate=" + $txt_endDate;
}
function getQuarterStartMonth() {
    var nowMonth = new Date().getMonth() + 1;
    var quarterStartMonth = 0;
    if (nowMonth < 3) {
        quarterStartMonth = 0;
    }
    if (2 < nowMonth && nowMonth < 6) {
        quarterStartMonth = 3;
    }
    if (5 < nowMonth && nowMonth < 9) {
        quarterStartMonth = 6;
    }
    if (nowMonth > 8) {
        quarterStartMonth = 9;
    }
    return quarterStartMonth;
}