using capstone_project_be.Application.DTOs;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    //Kiểu request mà controller sẽ dùng để gửi đi cho handler xử lý
    public class SignUpRequest(UserSignUpDTO userSignUpData) : IRequest
    {
        //nếu request này muốn nhận tham số thì dùng constructor
        //trong trường hợp này cần nhận dữ liệu đăng ký từ người dùng

        //constructor bình thường
        //public UserSignUpDTO UserSignUpData {  get; set; }

        //public SignUpRequest(UserSignUpDTO userSignUpData)
        //{
        //    UserSignUpData = userSignUpData;
        //}

        //primary constructor (ngắn gọn hơn, b dùng cái nào cx dc)
        public UserSignUpDTO UserSignUpData { get; set; } = userSignUpData;
    }
}
