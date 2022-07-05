using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity> where MISAEntity : class
    {
        IBaseRepository<MISAEntity> _baseRepository;

        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public int Insert(MISAEntity entity)
        {
            // Thực hiện Validate
            if (!Validate(null, entity))
            {
                return 0;
            }

            // Sau khi xử lý nghiệp vụ xong:
            var res = _baseRepository.Insert(entity);

            return res;
        }

        public int Update(Guid entityId, MISAEntity entity)
        {
            // Thực hiện Validate
            if (!Validate(entityId, entity))
            {
                return 0;
            }

            // Sau khi xử lý nghiệp vụ xong:
            var res = _baseRepository.Update(entityId, entity);

            return res;
        }

        private bool Validate(Guid? entityId, MISAEntity entity)
        {
            // Lấy ra các prop của object
            var properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                // Lấy tên của prop
                var propName = property.Name;


                // Lấy giá trị của prop
                var propValue = property.GetValue(entity);

                // Lấy ra thông tin attribute displayName
                var displayNameAttribute = Attribute.GetCustomAttribute(property, typeof(MISADisplayName));

                string propDisplayName = "";

                // Lấy displayName
                if(displayNameAttribute != null)
                {
                    propDisplayName = (displayNameAttribute as MISADisplayName).DisplayName;
                }

                // 1.Kiểm tra xem prop có bắt buộc không
                var isRequired = Attribute.IsDefined(property, typeof(MISARequired));

                // Nếu prop bắt buộc mà để trống
                if (isRequired && (propValue == null || propValue.ToString() == String.Empty))
                {
                    // Lấy cảnh báo lỗi của attribute required
                    var requiredAttributeObject = Attribute.GetCustomAttribute(property, typeof(MISARequired));
                    var errorMsg = (requiredAttributeObject as MISARequired).ErrorMsg;


                    throw new ValidateException(errorMsg == null ? String.Format(Resources.ResourceVN.ErrorValidateEmpty, propDisplayName) : errorMsg, propName);
                }

                // 2.Check độ dài của prop

                // Lấy ra thông tin attribute length
                var lengthAttribute = Attribute.GetCustomAttribute(property, typeof(MISADataLength));

                int lengthProp = int.MaxValue;

                // Lấy độ dài prop
                if (lengthAttribute != null)
                {
                    lengthProp = (lengthAttribute as MISADataLength).Length;
                }

                // Nếu độ dài prop lớn hơn độ dài quy định
                if(propValue != null && propValue.ToString().Count() > lengthProp)
                {
                    throw new ValidateException(String.Format(Resources.ResourceVN.ErrorValidateLength, propDisplayName, lengthProp), propName);

                }

                // 3.Kiểm tra ngày tháng không được lớn hơn ngày hiện tại
                var isValidateDate = Attribute.IsDefined(property, typeof(MISAValidateDate));

                if(isValidateDate && propValue != null && (DateTime)propValue > DateTime.Now)
                {
                    throw new ValidateException(String.Format(Resources.ResourceVN.ErrorValidateDate, propDisplayName), propName);
                }

                // 4. Kiểm tra email có đúng định dạng không

                var isValidateEmail = Attribute.IsDefined(property, typeof(MISAEmailValidate));

                if (isValidateEmail && propValue != null && propValue.ToString() != String.Empty)
                {
                    var email = propValue.ToString();
                    var trimmedEmail = email.Trim();

                    if (trimmedEmail.EndsWith("."))
                    {
                        throw new ValidateException(Resources.ResourceVN.ErrorValidateEmail, propName);
                    }
                    try
                    {
                        var addr = new System.Net.Mail.MailAddress(email);
                    }
                    catch
                    {
                        throw new ValidateException(Resources.ResourceVN.ErrorValidateEmail, propName);
                    }
                }

            }

            ValidateCustom(entityId, entity);

            return true;
        }

        protected virtual bool ValidateCustom(Guid? entityId, MISAEntity entity)
        {
            return true;
        }

        public int Delete(List<string> entityIds)
        {
            string ids = "";
            foreach (string employeeId in entityIds)
            {
                if (ids.Count() > 0)
                {
                    ids += ",";
                }
                ids += employeeId;
            }
            var res = _baseRepository.Delete(ids);
            return res;
        }
    }
}


