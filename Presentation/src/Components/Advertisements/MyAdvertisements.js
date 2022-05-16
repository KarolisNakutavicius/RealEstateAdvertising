import React, {useEffect, useState} from 'react'
import { Link, useParams } from "react-router-dom";
import Advertisement from './Advertisement';
import AdvertisementService from '../../Services/AdvertisementService'

export default function MyAdvertisements() {
    
    const [ads, setAds] = useState([]);
    
    let { id } = useParams();
    const [pageIndex,setPageIndex] = useState(parseInt(id));
    
    const [pageInfo, setPageInfo] = useState({
        totalPages:0,
        pageSize:2,
        totalRecordsCount:0
    })

    const [message, setMessage] = useState("Loading...");

    useEffect(async () => {
        debugger;
        let pagedAds = await AdvertisementService.getMyAdvertisments(pageIndex, pageInfo.pageSize);

        if (pagedAds.items.length > 0) {
            setAds(pagedAds.items)
            setPageInfo({
                totalPages:pagedAds.totalPages,
                pageSize:pagedAds.pageSize,
                totalRecordsCount:pagedAds.totalRecordsCount})
            return;
        }

        setMessage("You don't have any advertisements posted");
    }, [pageIndex])

    return (
        <>
            {ads.length === 0 && (
                <h3>{message}</h3>
            )}
            <div className='d-flex justify-content-start flex-wrap'>
                {ads.map(ad => {
                    return <Advertisement key={ad.id} ad={ad} isPersonal={false}/>
                })}
            </div>

            <nav aria-label="Page navigation example">
                <ul className="pagination">
                    <li className="page-item"><a className="page-link" href="#">Previous</a></li>
                    {Array(pageInfo.totalPages).fill(null).map((value, index) =>
                        <li className="page-item" key={index+1} onClick={(e) => setPageIndex(index + 1 )}
                        > 
                            <Link to={'/my-advertisements/' + (index + 1)} className="nav-link" >
                                <a>{index + 1}</a>
                            </Link>
                        </li>
                        )
                    }
                    <li className="page-item"><a className="page-link" href="#">Next</a></li>
                </ul>
            </nav>
        </>
    )
}
