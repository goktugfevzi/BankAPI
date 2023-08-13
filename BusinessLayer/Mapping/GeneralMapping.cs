using AutoMapper;
using DTOLayer.DTOs.Account;
using DTOLayer.DTOs.Bill;
using DTOLayer.DTOs.Card;
using DTOLayer.DTOs.Transaction;
using DTOLayer.DTOs.TransactionType;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Account, ResultAccountDto>().ReverseMap();
            CreateMap<Account, CreateAccountDto>().ReverseMap();
            CreateMap<Account, UpdateAccountDto>().ReverseMap();
            CreateMap<ResultAccountDto, UpdateAccountDto>().ReverseMap();
            CreateMap<Bill, ResultBillDto>().ReverseMap();            
            CreateMap<Bill, CreateBillDto>().ReverseMap();
            CreateMap<Bill, UpdateBillDto>().ReverseMap();
            CreateMap<Card, ResultCardDto>().ReverseMap();
            CreateMap<Card, CreateCardDto>().ReverseMap();
            CreateMap<Card, UpdateCardDto>().ReverseMap();
            CreateMap<Transaction, ResultTransactionDto>().ReverseMap();
            CreateMap<Transaction, CreateTransactionDto>().ReverseMap();
            CreateMap<Transaction, UpdateTransactionDto>().ReverseMap();
            CreateMap<TransactionType, ResultTransactionTypeDto>().ReverseMap();
            CreateMap<TransactionType, CreateTransactionTypeDto>().ReverseMap();
            CreateMap<TransactionType, UpdateTransactionTypeDto>().ReverseMap();
        }
    }
}
