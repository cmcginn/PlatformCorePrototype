var mockData = {
    
    view1: {
    filters: [
        {
            columnName: 'Code',
            displayName: 'Code',
            filterType: 0,
            selectionMode: 'multi',
            filterValues: [
                { key: 'Account Main', value: 'Account Main', active: false },
                { key: 'Statistical', value: 'Statistical', active: false },
                { key: 'ACH Failed Fee Billed', value: 'ACH Failed Fee Billed', active: false },
                { key: 'ACH Failed Fee Collected', value: 'ACH Failed Fee Collected', active: false },
                { key: 'ACH Failed Fee Reversed', value: 'ACH Failed Fee Reversed', active: false },
                { key: 'ACH Failed Fee Voided', value: 'ACH Failed Fee Voided', active: false },
                { key: 'Active families', value: 'Active families', active: false },
                { key: 'Active families that made at least one successful payment', value: 'Active families that made at least one successful payment', active: false },
                { key: 'Amount', value: 'Amount', active: false },
                { key: 'Average Admin Fee', value: 'Average Admin Fee', active: false }
            ],

        }

    ],
        queryBuilder: {
            selectedFilters: [],
            collectionName:'segments'
        }
    }
}