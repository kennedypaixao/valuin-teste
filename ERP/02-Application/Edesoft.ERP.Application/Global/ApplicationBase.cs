using AutoMapper;
using Edesoft.ERP.Domain.Interfaces;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Data.Entity;
using System.Linq;

namespace Edesoft.ERP.Application.Global
{
    public class ApplicationBase
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

		public ApplicationBase()
        {
            _unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>();
			_mapper = ServiceLocator.Current.GetInstance<IMapper>();

		}

        protected IMapper Mapper { get { return _mapper; } }

		protected void SaveChanges()
        {
            _unitOfWork.SaveChanges();
            _unitOfWork.RefreshInstanceContext();
        }

        protected void BeginTransaction()
        {
            _unitOfWork.BeginTransaction();
        }

        protected int Commit()
        {
            return _unitOfWork.Commit();
        }

        protected void Rollback()
        {
            _unitOfWork.Rollback();
        }

    }
}
