using System;

public class GetDictionaryDTO<T>
{
	public List<T> Dictionary { get; set; }
	public PaginationDTO Pagination { get; set; }
}
