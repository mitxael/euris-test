select a.* from ProdottoSet a
left join DettaglioListinoSet b on a.Id = b.Id_prodotto
where (b.Id_prodotto is NULL OR b.Id_listino != 1)