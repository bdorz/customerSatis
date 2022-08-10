<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CSQ.aspx.cs" Inherits="customerSatis.CSQ" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .container {
            display: flex;
            flex-direction: column;
            align-items: center;
        }
        /*外層DIV */
        .outerDiv {
            background-color: #1E2973;
            width: 550px;
            height: 550px;
        }
        /*內層DIV 與外層一套做出上下藍條*/
        .innerDiv {
            background-color: #ffffff;
            width: 550px;
            height: 525px;
            margin-top: 12px;
        }

        /*亞昕圖片*/
        .topImg {
            padding-right: 100px;
            padding-top: 20px;
            padding-left: 80px;
        }

        .titleLab {
            font-size: 48px;
            font-weight: bold;
        }

        .titleDiv {
            padding-left: 80px;
        }

        .table {
            padding-left: 80px;
        }

        .tbArea {
            font-size: 20px;
            border-radius: 20px;
            background-color: #d9d9d9;
            padding: 10px;
        }

        .btn {
            user-select: none;
            padding: 2px 13px;
            font-size: 18px;
            line-height: 28px;
            min-height: 35px;
            border-radius: 12px;
            letter-spacing: 2px;
            width: 200px;
            background-color: #1E2973;
            color: aliceblue;
            margin-left: 200px;
        }

            .btn:hover, .btn:active {
                background-color: #3d467a;
            }

        .star {
            display: inline-block;
            height: 1em;
            line-height: 1em;
            font-size: 70px;
            padding-top: 10px;
            margin-left: 25px;
        }

            .star input:first-child {
                display: none;
            }

            .star input {
                display: block;
                float: left;
                margin: 0;
                padding: 0;
                width: 1em;
                height: 1em;
                font: inherit;
                background: center 0/cover no-repeat;
                outline: 0 none transparent;
                -webkit-appearance: none;
                -moz-appearance: none;
                appearance: none;
            }

                .star input:checked ~ input {
                    background-position: center -1em;
                }

            .star input {
                background-image: url("./img/star2.PNG");
            }
            .custForm{     
                padding-top:calc(100vh / 2 - 275px);                   
            }
        @media (max-width: 700px) {
            /*外層DIV */
            .outerDiv {
                width: 100%;
            }
            /*內層DIV 與外層一套做出上下藍條*/
            .innerDiv {
                width: 100%;
            }
            /*亞昕圖片*/
            .topImg {
                padding-left: 30px;
            }

            .titleLab {
                font-size: 30px;
            }

            .titleDiv {
                padding-left: 30px;
            }

            .table {
                padding-left: 30px;
            }

            .tbArea {
                font-size: 20px;
                width: 85%;
            }

            .star {
                font-size: 50px;
                margin-left: 15px;
            }

            .btn {
                margin-top: 2.5%;
                width: 50%;
                margin-left: 95px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="custForm" >
        <div id="content" class="container">
            <div class="outerDiv">
                <div class="innerDiv">
                    <div class="topImg">
                        <%-- 套上公司LOGO --%>
                      <%--  <asp:Image ID="Image1" ImageUrl="" Width="230" runat="server" />--%>
                    </div>
                    <div class="titleDiv">
                        <asp:Label ID="Label3" CssClass="titleLab" runat="server" Text="服務滿意度調查"></asp:Label>
                        <br />
                        <asp:Label ID="titleDescribe" Font-Size="14" runat="server" Text="(關於本次報修案件處理的滿意程度)"></asp:Label>
                    </div>
                    <table class="table">
                        <tbody>
                            <tr>
                                <td>
                                    <div id="starDiv" class="star">
                                        <input type="radio" name="rdStarLabel" checked="checked" />
                                        <input type="radio" name="rdStarLabel" runat="server" id="scoreOne" onclick="starClick()" value="1" />
                                        <input type="radio" name="rdStarLabel" runat="server" id="scoreTwo" onclick="starClick()" value="2" />
                                        <input type="radio" name="rdStarLabel" runat="server" id="scoreThree" onclick="starClick()" value="3" />
                                        <input type="radio" name="rdStarLabel" runat="server" id="scoreFour" onclick="starClick()" value="4" />
                                        <input type="radio" name="rdStarLabel" runat="server" id="scoreFive" onclick="starClick()" value="5" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div>
                                        <div>
                                            <textarea id="tbCustSay" class="tbArea" rows="8" cols="35" placeholder="(請問您有任何的意見或建議嗎？)"></textarea>
                                            <asp:HiddenField ID="hfScore" runat="server" />
                                            <asp:HiddenField ID="hfcode" runat="server" />
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div>
                      
                        <asp:Button ID="btnConfirm" runat="server" Text="送出" CssClass="btn" OnClientClick="toSave();" />
                    </div>
                </div>
            </div>

        </div>
    </form>
    <script type="text/javascript">
        function toSave() {
            var code = document.getElementById('hfcode');
            var score = document.getElementById("hfScore");
            var custSay = document.getElementById('tbCustSay');
            var strPrame = 'code=' + code.value + '&score=' + score.value + '&CustSay=' + custSay.value;
            try {
                var fontclient = new XMLHttpRequest();
                fontclient.onreadystatechange = function () {
                    try {
                        if (fontclient.readyState == 4 && fontclient.status == 200) {
                            var response = fontclient.responseText;
                            alert("感謝您填寫寶貴的意見");
                            window.close();
                        }
                        else {
                            if (fontclient.readyState == 4 && fontclient.status == 403) {
                                alert("無權限");
                            }
                        }
                    }
                    catch (e) {
                    }
                }
                fontclient.open("POST", encodeURI("saveSQl.ashx"), false);
                fontclient.send(strPrame);
            }
            catch (e) {
            }
        }

        function starClick() {
            var starDiv = document.getElementById("starDiv");
            if (starDiv != null) {
                var child = starDiv.getElementsByTagName("input");
                for (var i = 0; i < child.length; i++) {
                    if (child[i] != null) {
                        if (child[i].tagName.toLowerCase() == "input") {
                            var rbl = child[i];
                            if (rbl.checked == true) {
                                var hfScore = document.getElementById("hfScore");
                                hfScore.value = rbl.value;
                            }
                        }
                    }
                }
            }
        }
    </script>
</body>
</html>
