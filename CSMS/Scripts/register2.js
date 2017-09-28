$(function () {
        $('form').bootstrapValidator({
　　　　　　　   　message: 'This value is not valid',
	        feedbackIcons:{
	        	valid:'glyphicon glyphicon-ok',
	        	invalid:'glyphicon glyphicon-remove',
	        	validating:'glyphicon glyphicon-refresh'
	        },
            fields: {
                ContractName: {
                    validators: {
                        notEmpty: {
                            message: '合同名不能为空'
                        },
                        //remote: {  //（应该是所以的验证通过才）发送AJAX请求到后台
						//	type: 'post',
						//	url: '/MM/AjaxTT',
						//	message: '合同名已存在',
						//	delay: 1000  //每隔1秒发送一次ajax请求
					    //   },
                        //emailAddress: {
                        //    message: '邮箱地址格式有误'
                        //}
                     
	                   
                    }
                },
                Contract_Amount: {
                    validators: {
                        notEmpty: {
                            message: '用户名不能为空'
                        },
                        stringLength: {
                            min: 3,
                            max: 6,
                            message: '用户名长度必须在3到6位之间'
                        },
                        regexp: {
                            regexp: /^\w+$/,
                            message: '用户名只能包含大写、小写、数字和下划线'
                        }
                    }
                },
                //username: {
                //    validators: {
                //        notEmpty: {
                //            message: '用户名不能为空'
                //        },
                //        stringLength: {
                //            min: 6,
                //            max: 18,
                //            message: '用户名长度必须在6到18位之间'
                //        },
                //        remote: {  //（应该是所以的验证通过才）发送AJAX请求到后台
				//			type: 'post',
				//			url: '/BLzhaoping/qiyezhuceajax',
				//			message: '此用户名已注册',
				//			delay: 1000  //每隔1秒发送一次ajax请求
				//	       },
                //        regexp: {
                //            regexp: /^[0-9]+$/,
                //            message: '用户名只能包含大写、小写、数字和下划线'
                //        }
                //    }
                //},
                //pass: {
                //    validators: {
                //        notEmpty: {
                //            message: '密码不能为空'
                //        },
                //        stringLength: {
                //            min: 6,
                //            max: 18,
                //            message: '密码长度必须在6到18位之间'
                //        },
                       
                //        regexp: {
                //            regexp: /^[a-zA-Z0-9_]+$/,
                //            message: '用户名只能包含大写、小写、数字和下划线'
                //        }
                //    }
                //},
                //repass: {
                //    validators: {
                //        notEmpty: {
                //            message: '密码不能为空'
                //        },
                //        regexp: {
                //            regexp: /^[a-zA-Z0-9_]+$/,
                //            message: '用户名只能包含大写、小写、数字和下划线'
                //        },
                //        identical: {
				//			field: 'pass',
				//			message: '*两次输入密码不一致'
				//		}
                //    }
                //},
                //phone :{
                //	validators: {
                //		notEmpty: {
                //            message: '电话不能为空'
                //        },
                //        regexp: {
                //            regexp: /^1[0-9]{10}$/,
                //            message: '电话格式不正确'
                //        }
                //    }
                //}
            }
        }).on("success.form.bv",function(e){
			alert("sucess");
		});
    });