var user = {
    init: function () {
        user.loadProvice();
        user.registrerEvent();
      
    },
    loadProvice: function () {
       
        $.ajax({
            url: '/TestDropdown/LoadProvince',
            type: "POST",
            dataType: "Json",
            success: function (response) {
                if (response.status = true)
                {
                    var html = '<option value="">--Chọn tỉnh thành--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="'+item.ID+'">'+item.Name+'</option>'
                    });
                    $('#ddlProvince').html(html);
                }
            }
        })
    },
    registrerEvent: function () {
        $('#ddlProvince').off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                user.loadDistrict(parseInt(id));
            }
            else {
                $('#ddlDistrict').html('');
            }
            $('#ddlPrecinct').html(''); // Xóa danh sách xã/phường khi thay đổi tỉnh
        });

        $('#ddlDistrict').off('change').on('change', function () { // Sửa đổi này
            var id = $(this).val();
            if (id != '') {
                user.loadPrecinct(parseInt(id));
            }
            else {
                $('#ddlPrecinct').html('');
            }
        });
   

    },
    loadDistrict: function (id) {

        $.ajax({
            url: '/TestDropdown/LoadDistrict',
            type: "POST",
            data: {provinceID:id},
            dataType: "Json",
            success: function (response) {
                if (response.status = true) {
                    var html = '<option value="">--Chọn quận huyện--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlDistrict').html(html);
                }
            }
        })
    },
    
    loadPrecinct: function (id) {

        $.ajax({
            url: '/TestDropdown/LoadPrecinct',
            type: "POST",
            data: { districtID: id },
            dataType: "Json",
            success: function (response) {
                if (response.status = true) {
                    var html = '<option value="">--Chọn xã phường--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>'
                    });
                    $('#ddlPrecinct').html(html);
                }
            }
        })
    }
}
user.init();