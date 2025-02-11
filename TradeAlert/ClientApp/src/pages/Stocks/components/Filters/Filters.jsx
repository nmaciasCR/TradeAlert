import React, { useEffect, useState, useCallback } from "react";
import Styles from "./Filters.css";
import Accordion from 'react-bootstrap/Accordion';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import GroupFilter from '../GroupFilter/GroupFilter';
import MarketFilter from '../MarketFilter/MarketFilter';
import { SelectFilterList } from "../SelectFilterList/SelectFilterList";
import { Link } from 'react-router-dom';



const Filters = ({ stockFiltersList, fnGroupFilters, fnMarketFilters, fnStatusSelectFilter, statusSelectedValueFilter, fnResetFiltersEvent }) => {



    //Retorna un titulo para los filters
    const subTtitle = (name) => {

        return (<div className="sub-title">{name}</div>)
    }

    return (
        <div className="container-filters">
            <Accordion>
                <Accordion.Item eventKey="0">
                    <Accordion.Header>Filtros <Link to="#" className="reset-filters-link" onClick={fnResetFiltersEvent}>Reestablecer</Link></Accordion.Header>
                    <Accordion.Body>
                        <div className="container-fluid">
                            <Row>
                                <Col sm={4}>
                                    {subTtitle('Grupos')}
                                    <GroupFilter stockFiltersList={stockFiltersList} fnGroupFilters={fnGroupFilters} />
                                </Col>
                                <Col sm={4}>
                                    {subTtitle('Mercados')}
                                    <MarketFilter stockFiltersList={stockFiltersList} fnMarketFilters={fnMarketFilters} />
                                </Col>
                                <Col sm={4}>
                                    {subTtitle('Estado')}
                                    <SelectFilterList valueSelected={statusSelectedValueFilter} onChangeEvent={fnStatusSelectFilter} />
                                </Col>
                            </Row>
                        </div>


                    </Accordion.Body>
                </Accordion.Item>
            </Accordion>
        </div>
    )
}


export default Filters;