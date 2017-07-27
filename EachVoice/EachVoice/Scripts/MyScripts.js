//--ZM implement the comments, votes.
$(document).ready(function () {
    var str = "";
    var actionStr = "";
    var sourceStr = "";
    var voteStr = "";
    var tblstr = "";
    var tr = "";
    var appvote = "";
    var dappvote = "";
    var k = '@System.Configuration.ConfigurationManager.AppSettings["ostate"]';
    $('#searchkey').click(function (e)
    {       
        e.preventDefault();
        var key = $('#key').val();
        var state = "wa";
        var ttl = "";
        var bill_id = "";
        $('#fromTag').html('');
        $('#fromTag').addClass('jumbotron');
        $.getJSON("https://openstates.org/api/v1/bills/?state="+state+"&subject="+key+"&apikey="+k, function (data) {
            $.each(data,
                function () {
                    
                    $('#fromTag').append("<a class='url' href='#' id=\"" + this.id +"\" data-title=\""+ this.title +"\">" + this.title + "</a> <br /><br />");                    
                });
            $('.url').click( function (e) {
                e.preventDefault();
                bill_id = $(this).attr('id');
                ttl = $(this).attr('data-title');
                console.log(ttl);
                console.log(bill_id);
                //this is what i need(; 
                //$(document).click(function () { $('#fromTag').html(''); $('#fromTag').removeClass('jumbotron'); });
                $.getJSON("https://openstates.org/api/v1/bills/" + bill_id+"/?apikey="+k, function (data) {
                    
                    $('#fromTag').html('');

                    
                   //--ZM******allow extra time for ajext to finish its job, display data properly************
                    $.ajax({
                        async: false,
                        url: "http://localhost:53764/Home/Vote?bill_id=" + bill_id + "&votebit",
                        type: 'post',
                        success: function (data) {
                            if (!data.approve) { appvote = 0; }
                            else { appvote = data.approve; }
                            if (!data.disapprove) { dappvote = 0; }
                            else { dappvote = data.disapprove; }
                        }
                    });
                    console.log("approve: " + appvote + " disapprove: " + dappvote);
                    //--ZM **** cache var first to create jquery variable then use it *****
                   var temp1 =$("<br /><a class='btn btn-danger' href='/Home/CreateComments?bill_id=" + bill_id + "&ttl=" + ttl + "'" + "class='text-danger h3'>Join the discussion</a>");
                   var temp2 = $("<span style='padding-left: 100px'><a id='appr' class='text-success' href='#'>Approve</a>&nbsp&nbsp<label id='approve'>"+ appvote +"</label></span>");
                   var temp3 = $("<span style='padding-left: 90px'><a id='disappr' class='text-success' href='#'>Disapprove</a>&nbsp&nbsp<label id='disapprove'>" + dappvote + "</label></span><br /><br /><br />");
                   $('#fromTag').append(temp1);
                   $('#fromTag').append(temp2);
                   $('#fromTag').append(temp3);
                   
                    $('#appr').click(function (e) {
                        e.preventDefault();
                        
                        console.log("id: " + $(this).attr('id'));
                        $.ajax({
                            url: 'Vote?bill_id='+bill_id+'&votebit=0',
                            type: 'POST',
                            //data:{bill_id:bill_id,votebit:0},
                            success: function (data) {
                                console.log("approve: " + data.approve + "disapprove: " + data.disapprove);
                                $('#approve').text(data.approve);
                                $('#disapprove').text(data.disapprove);
                            },
                            error: function(){alert("something went wrong");}
                        });
                        
                    });
                    $('#disappr').click(function (e) {
                        e.preventDefault();


                        console.log("id: " + $(this).attr('id'));
                        $.ajax({
                            url: 'Vote?bill_id=' + bill_id + '&votebit=1',
                            type: 'POST',
                            //data:{bill_id:bill_id,votebit:0},
                            success: function (data) {
                                console.log("approve: " + data.approve + "disapprove: " + data.disapprove);
                                $('#approve').text(data.approve);
                                $('#disapprove').text(data.disapprove);
                            },
                            error: function () { alert("something went wrong"); }
                        });
                    });

                    str += "<h3>Session:&nbsp" + data.session + "&nbsp&nbsp&nbsp&nbsp Id:&nbsp" + data.id + "</h3><br />";

                    sourceStr += "<h3>Sources: </h3><br />";
                    $.each(data.sources, function (i, item) {
                        sourceStr += "<a target=_'blank' href='" + item.url + "'>" + item.url + "</a><br />"
                        
                    });
                    //str += sourceStr;

                    voteStr += "<h3>Votes: </h3><br />";
                    $.each(data.votes, function (i, item) {
                        voteStr += "<p>Yes_Count:&nbsp" + item.yes_count + "&nbsp&nbsp&nbsp&nbspId:&nbsp" + item.id + "<br />"
                    });
                    //str += voteStr;

                    actionStr += "<h3>Actions: </h3><br />";
                    actionStr += "<table class='table text-center table-stripe'><tr><td>Date</td><td>Action</td><td>actor</td></tr>";
                    $.each(data.actions, function (i, item) {
                        actionStr+="<tr><td>"+item.date+"</td><td>"+item.action+"</td><td>"+item.actor+"</td></tr>"
                    });


                    str += sourceStr + voteStr+actionStr;
                    
                    $('#fromTag').addClass('jumbotron');
                    $('#fromTag').append(str);
                    str = "";
                    sourceStr = "";
                    voteStr = "";
                    actionStr = "";

                    
                });
                
            })
        });
        return false;
    });
  
  
    
    //$(document).click(function () { $('#fromTag').html(''); $('#fromTag').removeClass('jumbotron'); });
    
});