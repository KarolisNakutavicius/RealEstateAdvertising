import React, {Component, useState, useEffect, useRef} from 'react'
import Advertisement from '../Advertisements/Advertisement';
import Filters from './Filters';
import Collapse from "react-bootstrap/Collapse";
import Button from 'react-bootstrap/Button'
import PagingBar from "../General/PagingBar";
import AdvertisementService from "../../Services/AdvertisementService";

export default function Home({ads, getAds, setAds, setFilters, filters, sortBy, setSortBy}) {
    
    const [message, setMessage] = useState("Loading ...");
    const [isFiltersOpen, setIsFiltersOpen] = useState(false)
    
    const [pageIndex, setPageIndex] = useState(ads.currentPage);
    
    useEffect(() => {
        setAds({...ads,currentPage:pageIndex});
    }, [pageIndex])
    
    useEffect(() => {
        if(ads.items.length > 0){
            setMessage("")
            return;
        }

        setMessage("There are no advertisements posted yet");
    }, [ads.items])
    
    function handleOpen() {
        setIsFiltersOpen(!isFiltersOpen);
    }

    async function saveAd(id){
        return await AdvertisementService.saveAdvertisement(id);
    }

    return (
        <>
            <div className="row">
                <div className="col-2 mx-4">
                    
                </div>
                <div className="col-3 mx-1">
                    <label> <b>Sort By :</b></label>
                </div>
            </div>
            
            <div className="row">
                <Button className="col-2 mx-4"
                    onClick={handleOpen}
                    aria-controls="dropdown"
                    aria-expanded={isFiltersOpen}>
                    Open filters <i className={!isFiltersOpen ? "fa fa-angle-double-down" : "fa fa-angle-double-up"} style={{fontSize: '24px', margin: "3px 0 0 6px"}}></i>
                </Button>

                <div className="col-3">
                    <select className="form-select"
                             value={sortBy}
                             onChange={(e) => setSortBy(parseInt(e.target.value))}
                            style={{width:"auto", display:"inline-block"}}>
                        <option value="0">Best Match</option>
                        <option value="1">Price</option>
                        <option value="2">Size</option>
                    </select>
                </div>
            </div>

            
            <Collapse in={isFiltersOpen}>
                <div className='mt-1' id="dropdown">
                    <Filters getAds={getAds} setFilters={setFilters} filters={filters}/>
                </div>
            </Collapse>
            



            
            {ads.items.length === 0 && (
                <h3 className="m-5">{message}</h3>
            )}
            {ads.items.length > 0 &&
                (
                    <div className='mt-4 d-flex justify-content-start flex-wrap'>
                        {ads.items.map(ad => {
                            return <Advertisement key={ad.id} ad={ad} isPersonal={true} saveAd={saveAd}/>
                        })}
                    </div>   
                )}
            
            <PagingBar setPageIndex={setPageIndex} totalPages={ads.totalPages} pageIndex={pageIndex}/>
        </>
    )
}
