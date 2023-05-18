using StudentProject.Dto.RequestDto;
using StudentProject.Dto.ResponseDto;
using StudentProject.Models;

namespace StudentProject.Transformer
{
    public class StandardTransformer
    {
        public static Standard StandardRequestDtoToStandard(StandardRequestDto standardRequestDto)
        {
            Standard standard = new Standard();
            standard.StandardName = standardRequestDto.StandardName;
            standard.StandardDescription = standardRequestDto.StandardDescription;
            return standard;
        }
        public static StandardResponseDto StandardToStandardResponseDto(Standard standard)
        {
            StandardResponseDto standardResponseDto = new StandardResponseDto();
            standardResponseDto.StandardName = standard.StandardName;
            standardResponseDto.Message =" ";
            return standardResponseDto;

        }
    }
}
