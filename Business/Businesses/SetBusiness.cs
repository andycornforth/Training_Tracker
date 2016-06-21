using Exceptions;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface ISetBusiness
    {
        void AddSetToLog(Set set);
        IList<Set> GetSetsByLogId(int logId);
        Set GetLatestSetForLog(int LogId);
        void DeleteSet(int logId, int setId);
    }

    public class SetBusiness : ISetBusiness
    {
        private ISetRepository _setRepository;

        public SetBusiness(ISetRepository setRepository)
        {
            _setRepository = setRepository;
        }

        public void AddSetToLog(Set set)
        {
            if (set == null)
                throw new BusinessException("Set not specified");
            if (set.Log == null)
                throw new BusinessException("The set is not associated to a log");
            if (set.Exercise == null)
                throw new BusinessException("The set is not associated to an exercise");
            if (set.PositionInLog < 1)
                throw new BusinessException("The sets position in the log must be greater than 0");

            _setRepository.AddSet(set);
        }

        public IList<Set> GetSetsByLogId(int logId) => _setRepository.GetSetsByLogId(logId);

        public Set GetLatestSetForLog(int LogId) => _setRepository.GetLatestSetForLog(LogId);

        public void DeleteSet(int logId, int setId) => _setRepository.DeleteSet(logId, setId);
    }
}