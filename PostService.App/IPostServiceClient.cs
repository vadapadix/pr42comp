using PostService.Dtos;

namespace PostService.App;

public interface IPostServiceClient
{
    Task<PostingGetDto[]> GetPostingsAsync();
    Task<PostingGetDto> GetPostingByIdAsync(int id);
    Task<PostingGetDto> CreatePostingAsync(PostingPostDto dto);
    Task<PostingGetDto> UpdatePostingAsync(int id, PostingPutDto dto);
    Task DeletePostingAsync(int id);
}
