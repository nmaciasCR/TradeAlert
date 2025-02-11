import React, { useEffect, useState, useCallback } from "react";
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
import Filters from "./components/Filters/Filters";


let stockFilters = {
    type: TODO,
    groups: [],
    markets: []
}


const Stocks = () => {
    const [stocksList, setStockList] = useState([]);
    const [stocksDisplayList, setStockDisplayList] = useState([]);
    //Filtros para listado de acciones
    const [stockFiltersList, setStockFiltersList] = useState(stockFilters);

    //evento del filtro de tipo
    const onChangeSelectStockList = (event) => {
        //Actualizamos el filtro de tipo
        setStockFiltersList((prev) => {
            //Seleccionado (Los agregams si es que no esta)
            return { ...prev, type: parseInt(event.target.value) }
        });
    }

    //Evento del buscador
    const onChangeSearchInput = (event) => {
        searchStocks(event.target.value, stocksList);
    }


    //Aplicamos todos los filtros de las acciones
    function ApplyStockDisplayFilters() {
        //Listado de acciones temporal
        let listered = stocksList;


        //Filtro de tipo
        switch (stockFiltersList.type) {
            case TODO:
                listered = stocksList;;
                break;
            case REVISION:
                listered = stocksList.filter((s) => s.reviewRequired);
                break;
            default:
                alert('ERROR EN stockFiltersList.type');
                break;
        }


        //Filtro de grupos
        if (stockFiltersList.groups.length > 0) {
            listered = listered.filter(sg =>
                sg.groupsIdList.some(id => stockFiltersList.groups.includes(id))
            );
        }

        //Filtro de Mercados
        if (stockFiltersList.markets.length > 0) {
            listered = listered.filter(s =>
                stockFiltersList.markets.includes(s._market.id)
            );
        }





        //Listado final de acciones
        setStockDisplayList(listered);

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
                //Ordenamos las acciones por fecha de revision
                //Primeros las revisadas hace mas tiempo
                let sortedList = responseJson.sort((a, b) => new Date(a.dateReview) > new Date(b.dateReview) ? 1 : -1)
                setStockList(sortedList);
            })
            .catch(error => {
                console.log(error);
            })

    }, []);


    useEffect(() => {
        loadTableStocks();
    }, [loadTableStocks]);



    //Funcion que actualiza los grupos seleccionados en el filtro
    function UpdateGroupsFilters(event, groupId) {
        //Check o uncheck
        let isChecked = event.target.checked;

        setStockFiltersList((prev) => {
            if (isChecked) {
                //Seleccionado (Los agregams si es que no esta)
                if (!prev.groups.includes(groupId)) {
                    return { ...prev, groups: [...prev.groups, groupId] }
                }

            } else {
                //Deseleccionado
                return {
                    ...prev,
                    groups: prev.groups.filter(id => id !== groupId) // Filtra el grupo eliminado
                };
            }
        });
    }

    //Funcion que actualiza los mercados seleccionados en el filtro
    function UpdateMarketsFilters(event, marketId) {
        //Check o uncheck
        let isChecked = event.target.checked;

        setStockFiltersList((prev) => {
            if (isChecked) {
                //Seleccionado (Los agregams si es que no esta)
                if (!prev.markets.includes(marketId)) {
                    return { ...prev, markets: [...prev.markets, marketId] }
                }

            } else {
                //Deseleccionado
                return {
                    ...prev,
                    markets: prev.markets.filter(id => id !== marketId) // Filtra el market eliminado
                };
            }
        });
    }

    //Funcion para link de reestablecer los filtros
    function ResetFiltersEvent(event) {
        event.stopPropagation(); // Detiene la propagación del evento
        //Retet de filtros con el original
        setStockFiltersList(stockFilters);
    }



    // Ejecuta ApplyStockDisplayFilters() cuando stockFiltersList cambie
    useEffect(() => {
        ApplyStockDisplayFilters();
    }, [stockFiltersList, stocksList]);


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
                            <Filters
                                stockFiltersList={stockFiltersList}
                                fnGroupFilters={UpdateGroupsFilters}
                                fnMarketFilters={UpdateMarketsFilters}
                                fnStatusSelectFilter={onChangeSelectStockList}
                                statusSelectedValueFilter={stockFiltersList.type}
                                fnResetFiltersEvent={ResetFiltersEvent }
                            />
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