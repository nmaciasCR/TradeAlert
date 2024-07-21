﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TradeAlert.Business.Interfaces;
using TradeAlert.Business.Request;

namespace TradeAlert.Business
{
    public class Portfolio : Interfaces.IPortfolio
    {
        private Data.Entities.TradeAlertContext _dbContext;
        private readonly IMapper _mapper;
        private Interfaces.IStocks _businessStocks;


        public Portfolio(Data.Entities.TradeAlertContext dbContext, IMapper mapper, Interfaces.IStocks businessStocks)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _businessStocks = businessStocks;
        }

        public List<Data.Entities.Portfolio> GetList()
        {
            List<Data.Entities.Portfolio> listPortfolio = new List<Data.Entities.Portfolio>();

            try
            {
                listPortfolio = _dbContext.Portfolio
                    .Include(p => p.quote)
                    .Include(p => p.quote.QuotesAlerts)
                    .Include(p => p.quote.market)
                    .Include(p => p.quote.currency)
                    .ToList();

                return listPortfolio;


            }
            catch
            {
                return listPortfolio;
            }


        }


        /// <summary>
        /// Mapea un objeto portfolio en su correspondiente DTO
        /// </summary>
        public DTO.PortfolioDTO MapToDTO(Data.Entities.Portfolio portfolio)
        {
            try
            {
                DTO.PortfolioDTO DTOReturn = _mapper.Map<DTO.PortfolioDTO>(portfolio);
                DTOReturn.euroTotalAmount = portfolio.euroTotalAmount;

                return DTOReturn;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        /// <summary>
        /// Mapea una lista de quotes en su correspondiente DTO
        /// </summary>
        public List<DTO.PortfolioDTO> MapToDTO(List<Data.Entities.Portfolio> portfolioList)
        {
            List<DTO.PortfolioDTO> listReturn = new List<DTO.PortfolioDTO>();
            portfolioList.ForEach(q => listReturn.Add(MapToDTO(q)));
            return listReturn;
        }


        /// <summary>
        /// Actualiza los datos de una accion del portfolio
        /// </summary>
        public ProblemDetails Update(Request.UpdatePortfolio pStock)
        {
            //Objeto de respuesta ProblemDetails
            ProblemDetails problemDetailsResponse = new ProblemDetails();
            Dictionary<string, string> errorDictionary = new Dictionary<string, string>();


            try
            {
                //Validamos si existe la empresa
                if (_businessStocks.GetQuote(pStock.quoteId) == null)
                {
                    errorDictionary.Add("quoteId_1", "La empresa seleccionada no existe");
                }
                //Validamos si la empresa seleccionada es parte del portfolio
                if (!GetList().Any(p => p.quoteId == pStock.quoteId))
                {
                    errorDictionary.Add("quoteId_2", "La empresa seleccionada no existe en el portfolio");
                }
                //Validamos la cantidad de acciones
                if (pStock.quantity < 1)
                {
                    errorDictionary.Add("quantity", "La cantidad de acciones debe ser mayor que 0");
                }
                //Validamos el precio de la accion
                if (pStock.price <= 0.0)
                {
                    errorDictionary.Add("price", "El precio de compra de la acción debe ser mayor que 0");
                }

                //Si hay errores devolvemos el problemDetail
                if (errorDictionary.Any())
                {
                    problemDetailsResponse.Title = "Ha Ocurrido un error al actualizar acción en el portfolio";
                    problemDetailsResponse.Detail = "Los valores ingresados no sos correctos";
                    problemDetailsResponse.Type = "error-portfolio-update";
                    problemDetailsResponse.Status = StatusCodes.Status400BadRequest;
                    problemDetailsResponse.Extensions.Add("errors", errorDictionary);
                    return problemDetailsResponse;
                }


                //Validaciones OK
                //Actualizamos el portfolio
                _dbContext.Portfolio.Find(pStock.quoteId).quantity = pStock.quantity;
                _dbContext.Portfolio.Find(pStock.quoteId).averagePurchasePrice = pStock.price;
                _dbContext.SaveChanges();

                //Retornamos el problem detail ok
                problemDetailsResponse.Status = StatusCodes.Status200OK;
                return problemDetailsResponse;
            }
            catch
            {
                problemDetailsResponse.Status = StatusCodes.Status500InternalServerError;
                return problemDetailsResponse;
            }

        }

        /// <summary>
        /// Eliminar una accion del portfolio
        /// </summary>
        public Boolean Delete(int idPortfolio)
        {
            try
            {
                Data.Entities.Portfolio portfolioToDelete = _dbContext.Portfolio.Find(idPortfolio);
                _dbContext.Portfolio.Remove(portfolioToDelete);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Retorna el porcentaje que representa una accion
        /// respecto al portfolio
        /// </summary>
        public double GetWeightingPercent(double portfolioAmount, double stockAmount)
        {
            return (stockAmount / portfolioAmount) * 100;
        }

        /// <summary>
        /// Agregamos una accion al portfolio
        /// </summary>
        /// <param name="addPortfolio"></param>
        /// <returns></returns>
        public ProblemDetails Add(Request.AddPortfolio addPortfolio)
        {
            Data.Entities.Portfolio newPortfolio = new Data.Entities.Portfolio();
            ProblemDetails problemDetailsResponse = new ProblemDetails()
            {
                Title = "Ha Ocurrido un error al crear una nueva acción en el portfolio",
                Detail = "Los valores ingresados no sos correctos",
                Status = StatusCodes.Status200OK,
                Type = "error-portfolio-add",
            };
            Dictionary<string, string> errorDictionary = new Dictionary<string, string>();

            try
            {
                //Validamos si existe la empresa
                if (_businessStocks.GetQuote(addPortfolio.quoteId) == null)
                {
                    errorDictionary.Add("quoteId", "La empresa seleccionada no existe");
                }
                //Validamos si la empresa seleccionada ya es parte del portfolio
                if (GetList().Any(p => p.quoteId == addPortfolio.quoteId))
                {
                    errorDictionary.Add("quoteId", "La empresa seleccionada ya existe en el portfolio");
                }
                //Validamos la cantidad de acciones
                if (addPortfolio.quantity < 1)
                {
                    errorDictionary.Add("quantity", "La cantidad de acciones debe ser mayor que 0");
                }
                //Validamos el precio de la accion
                if (addPortfolio.price <= 0.0)
                {
                    errorDictionary.Add("price", "El precio de compra de la acción debe ser mayor que 0");
                }

                //Si hay errores devolvemos el problemDetail
                if (errorDictionary.Any())
                {
                    problemDetailsResponse.Extensions.Add("errors", errorDictionary);
                    problemDetailsResponse.Status = StatusCodes.Status400BadRequest;
                    return problemDetailsResponse;
                }

                //Agregamos la nueva accion al portfolio
                newPortfolio.quoteId = addPortfolio.quoteId;
                newPortfolio.quantity = addPortfolio.quantity;
                newPortfolio.averagePurchasePrice = addPortfolio.price;
                _dbContext.Portfolio.Add(newPortfolio);
                _dbContext.SaveChanges();

                //Incluimos el nuevo objeto en la respuesta de problemDetails
                problemDetailsResponse.Extensions.Add("result", MapToDTO(newPortfolio));

                return problemDetailsResponse;
            }
            catch
            {
                problemDetailsResponse.Status = StatusCodes.Status500InternalServerError;
                return problemDetailsResponse;
            }
        }

    }
}
