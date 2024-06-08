using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using ChattingSystem.Services.Interfaces;
using Microsoft.OpenApi.Validations;
using System.Reflection.Metadata.Ecma335;

namespace ChattingSystem.Services.Implements
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _participantRepository;
        public ParticipantService(IParticipantRepository participantRepository)
        {
            _participantRepository = participantRepository;
        }

        public async Task<IEnumerable<Participant>>? CreateMultiple(IEnumerable<Participant>? participant)
        {
            try
            {
                List<Participant> result = new List<Participant>();

                foreach (var item in participant)
                {
                    var temp = await _participantRepository.Create(item);
                    result.Add(temp);
                }
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<Participant>? Create(Participant? participant)
        {
            try
            {
                var result = await _participantRepository.Create(participant);
                return result;
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<IEnumerable<Participant>>? DeleteByConId(int? conId)
        {
            try
            {
                var result = await _participantRepository.DeleteByConId(conId);
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<Participant>? GetByConversationAndUserId(int? ConversationId, int? UserId)
        {
            try
            {
                var result = await _participantRepository.GetByConversationIdandUserId(ConversationId, UserId);
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<IEnumerable<Participant>>? GetByConversationId(int? Id)
        {
            var result = await _participantRepository.GetByConversationId(Id);
            return result;
        }
        public async Task<Participant>? GetByConversationIdObj(int? Id)
        {
            var result = await _participantRepository.GetByConversationIdObj(Id);
            return result;
        }

        public async Task<Participant>? GetById(int? Id)
        {
            var result = await _participantRepository.GetById(Id);
            return result;
        }

        public async Task<Participant>? DeleteByConIdAndUserId(int? conId, int? userId)
        {
            try
            {
                var result = await _participantRepository.DeleteByConIdAndUserId(conId, userId);
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<int>? GetUserId(int? participantId)
        {
            try
            {
                var result = await _participantRepository.GetUserId(participantId);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
