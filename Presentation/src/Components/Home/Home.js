import React, {Component, useState, useEffect, useRef} from 'react'
import Advertisement from '../Advertisements/Advertisement';
import Filters from './Filters';
import Collapse from "react-bootstrap/Collapse";
import Button from 'react-bootstrap/Button'
import PagingBar from "../General/PagingBar";

export default function Home({ads, getAds}) {
    
    const [message, setMessage] = useState("Loading ...");
    const [isFiltersOpen, setIsFiltersOpen] = useState(false);
    const [pageIndex, setPageIndex] = useState(ads.currentPage)
    
    useEffect(async () => {
        await getAds(null, pageIndex, ads.pageSize)
    },[pageIndex]);
    
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

    return (
        <>
            <Button
                onClick={handleOpen}
                aria-controls="dropdown"
                aria-expanded={isFiltersOpen}>
                Open filters <i className={!isFiltersOpen ? "fa fa-angle-double-down" : "fa fa-angle-double-up"} style={{fontSize: '24px', margin: "3px 0 0 6px"}}></i>
            </Button>
            <Collapse in={isFiltersOpen}>
                <div className='mt-1' id="dropdown">
                    <Filters getAds={getAds}/>
                </div>
            </Collapse>
            
            {ads.items.length === 0 && (
                <h3>{message}</h3>
            )}
            {ads.items.length > 0 &&
                (
                    <div className='mt-4 d-flex justify-content-start flex-wrap'>
                        {ads.items.map(ad => {
                            return <Advertisement key={ad.id} ad={ad} isPersonal={true}/>
                        })}
                    </div>   
                )}
            
            <PagingBar setPageIndex={setPageIndex} totalPages={ads.totalPages} pageIndex={pageIndex}/>
        </>
    )
}
