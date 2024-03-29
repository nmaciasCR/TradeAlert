﻿import React, { useEffect, useState, useCallback } from "react";
import Styles from "./Stocks.css";
import Header from "../../components/Header/Header";
import StocksTable from "./components/StocksTable/StocksTable";
import IndexMarket from "../../components/IndexMarket/IndexMarket";
import Summary from "./components/SummaryPriorities/SummaryPriorities";
import { TODO, REVISION, SelectFilterList } from "./components/SelectFilterList/SelectFilterList";
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import PortfolioTable from '../../components/PortfolioTable/PortfolioTable.jsx';
import SearchInput from "../../components/SearchInput/SearchInput.jsx";



let stockFilters = {
    type: TODO
}


const Stocks = () => {
    const [stocksList, setStockList] = useState([]);
    const [stocksDisplayList, setStockDisplayList] = useState([]);

    //evento del filtro
    const onChangeSelectStockList = (event) => {
        stockFilters.type = parseInt(event.target.value);
        filterStockList(stocksList);
    }

    //Evento del buscador
    const onChangeSearchInput = (event) => {
        searchStocks(event.target.value, stocksList);
    }


    //filtramos la lista de acciones que se van a mostrar
    const filterStockList = (stocks) => {
        switch (stockFilters.type) {
            case TODO:
                setStockDisplayList(stocks);
                break;
            case REVISION:
                setStockDisplayList(stocks.filter((s) => s.reviewRequired));
                break;
            default:
                alert('ERROR EN stockFilters.type');
                break;
        }

    }

    //Buscador de acciones por simbolo o nombre
    const searchStocks = (textToSearch, allStocksList) => {
        if (textToSearch == "") {
            setStockDisplayList(allStocksList);
        } else {
            setStockDisplayList(allStocksList.filter((s) => (s.symbol.toUpperCase().match(textToSearch.toUpperCase()) !== null)
                || (s.name.toUpperCase().match(textToSearch.toUpperCase()) !== null)));
        }
    }


    const loadTableStocks = useCallback(() => {
        fetch("api/Stocks/GetStocks")
            .then(response => { return response.json() })
            .then(responseJson => {
                //rdenamos las acciones por fecha de revision
                //Primeros las revisadas hace mas tiempo
                let sortedList = responseJson.sort((a, b) => new Date(a.dateReview) > new Date(b.dateReview) ? 1 : -1)
                setStockList(sortedList);
                filterStockList(sortedList);
            })
            .catch(error => {
                console.log(error);
            })

    }, []);


    useEffect(() => {
        loadTableStocks();
    }, [loadTableStocks]);

    return (
        <div>
            <Header />
            <IndexMarket />
            <div className="Container">
                <div className="container-fluid">
                    <Row>
                        <Col sm={2}>
                            <PortfolioTable />
                        </Col>
                        <Col sm={10}>
                            <div className="container-fluid">
                                <Row>
                                    <Col sm={4}>
                                        <div className="Title">Listado de Acciones ({stocksList.length})</div>
                                    </Col>
                                    <Col sm={5}>
                                        <Summary quotes={stocksList} />
                                    </Col>
                                    <Col sm={3} className="col-filter-table">
                                        <span className="filter-title">Filtrar </span>
                                        <SelectFilterList onChangeEvent={onChangeSelectStockList} />
                                    </Col>
                                </Row>
                                <Row>
                                    <Col sm={4} className="col-search-autocomplete">
                                        <span className="search-title">Buscador</span>
                                        <SearchInput omChange={onChangeSearchInput} />
                                    </Col>
                                </Row>
                            </div>
                            <br />
                            <StocksTable quotes={stocksDisplayList} refreshTableStocks={loadTableStocks} />
                        </Col>
                    </Row>
                </div>




            </div>
        </div>
    )
}


export default Stocks;